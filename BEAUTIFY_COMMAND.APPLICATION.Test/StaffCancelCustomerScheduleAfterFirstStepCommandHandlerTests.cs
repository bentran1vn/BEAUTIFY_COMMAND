using System.Linq.Expressions;
using BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;
using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Abstractions.Repositories;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using Moq;
using Xunit;
using MockQueryable.Moq;

namespace BEAUTIFY_COMMAND.APPLICATION.Test;
public class StaffCancelCustomerScheduleAfterFirstStepCommandHandlerTests
{
    private readonly Mock<IRepositoryBase<CustomerSchedule, Guid>> _customerScheduleRepositoryMock;
    private readonly Mock<IRepositoryBase<WalletTransaction, Guid>> _walletTransactionRepositoryMock;
    private readonly Mock<IRepositoryBase<WorkingSchedule, Guid>> _workingScheduleRepositoryMock;
    private readonly Mock<IRepositoryBase<Clinic, Guid>> _clinicRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly StaffCancelCustomerScheduleAfterFirstStepCommandHandler _handler;

    public StaffCancelCustomerScheduleAfterFirstStepCommandHandlerTests()
    {
        _customerScheduleRepositoryMock = new Mock<IRepositoryBase<CustomerSchedule, Guid>>();
        _walletTransactionRepositoryMock = new Mock<IRepositoryBase<WalletTransaction, Guid>>();
        _workingScheduleRepositoryMock = new Mock<IRepositoryBase<WorkingSchedule, Guid>>();
        _clinicRepositoryMock = new Mock<IRepositoryBase<Clinic, Guid>>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();

        _handler = new StaffCancelCustomerScheduleAfterFirstStepCommandHandler(
            _customerScheduleRepositoryMock.Object,
            _walletTransactionRepositoryMock.Object,
            _workingScheduleRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _clinicRepositoryMock.Object
        );
    }


    [Fact]
    public async Task Handle_ShouldRefundCorrectly_ForFirstStep()
    {
        // Arrange
        var command = new Command.StaffCancelCustomerScheduleAfterFirstStepCommand(Guid.NewGuid());

        var customerSchedule = new CustomerSchedule
        {
            Id = command.CustomerScheduleId,
            Status = Constant.WalletConstants.TransactionStatus.PENDING,
            Procedure = new Procedure
                { StepIndex = 1, Name = "Test Procedure", Service = new Service { Name = "Test Service" ,Description = "123"} },
            ProcedurePriceType = new ProcedurePriceType
                { Price = 100, Name = "Test Price Type", Duration = 10, IsDefault = true },
            Order = new Order { DepositAmount = 150 },
            Date = DateOnly.FromDateTime(DateTime.Now),
            OrderId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid()
        };

        var customer = new User
            { Email = "test", Password = "1", FirstName = "1", LastName = "1", Status = 1, Balance = 0 };
        customerSchedule.Customer = customer;

        // Create mock async queryable
        var mockDbSet = new List<CustomerSchedule> { customerSchedule }.AsQueryable().BuildMockDbSet();
        _customerScheduleRepositoryMock
            .Setup(x => x.FindAll(It.IsAny<Expression<Func<CustomerSchedule, bool>>?>()))
            .Returns(mockDbSet.Object);

        // For WorkingSchedule
        var emptyWorkingSchedules = new List<WorkingSchedule>().AsQueryable().BuildMockDbSet();
        _workingScheduleRepositoryMock
            .Setup(x => x.FindAll(It.IsAny<Expression<Func<WorkingSchedule, bool>>?>()))
            .Returns(emptyWorkingSchedules.Object);

        _clinicRepositoryMock
            .Setup(x => x.FindSingleAsync(It.IsAny<Expression<Func<Clinic, bool>>?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Clinic
            {
                Name = "1", Balance = 500, TaxCode = "1", BusinessLicenseUrl = "1", OperatingLicenseUrl = "1",
                OperatingLicenseExpiryDate = DateTimeOffset.Now,
                Address = "1", City = "1", District = "1", Ward = "1", Email = "1", PhoneNumber = "1", IsParent = true,
                BankName = "1", BankAccountNumber = "1"
            });

        _currentUserServiceMock
            .Setup(x => x.ClinicId)
            .Returns(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(50, customerSchedule.Customer.Balance);
        _customerScheduleRepositoryMock.Verify(x => x.Update(It.IsAny<CustomerSchedule>()), Times.Once);
        _walletTransactionRepositoryMock.Verify(x => x.Add(It.IsAny<WalletTransaction>()), Times.Once);
    }

  [Fact]
public async Task Handle_ShouldRefundCorrectly_ForLaterSteps()
{
    // Arrange
    var command = new Command.StaffCancelCustomerScheduleAfterFirstStepCommand(Guid.NewGuid());
    var orderId = Guid.NewGuid();
    var customerId = Guid.NewGuid();

    var customerSchedule = new CustomerSchedule
    {
        Id = command.CustomerScheduleId,
        Status = Constant.WalletConstants.TransactionStatus.PENDING,
        Procedure = new Procedure
            { StepIndex = 2, Name = "Test Procedure", Service = new Service { Name = "Test Service", Description = "123" } },
        ProcedurePriceType = new ProcedurePriceType
            { Price = 100, Name = "Test Price Type", Duration = 10, IsDefault = true },
        Order = new Order { DepositAmount = 150 },
        Date = DateOnly.FromDateTime(DateTime.Now),
        OrderId = orderId,
        CustomerId = customerId
    };

    var customer = new User
        { Email = "test", Password = "1", FirstName = "1", LastName = "1", Status = 1, Balance = 0 };
    customerSchedule.Customer = customer;

    // Add a related future schedule with the same OrderId
    var relatedSchedule = new CustomerSchedule
    {
        Id = Guid.NewGuid(), // Different ID
        Status = Constant.OrderStatus.ORDER_PENDING,
        Procedure = new Procedure
            { StepIndex = 3, Name = "Next Procedure", Service = new Service { Name = "Next Service", Description = "456" } },
        ProcedurePriceType = new ProcedurePriceType
            { Price = 50, Name = "Next Price Type", Duration = 10, IsDefault = true },
        Order = new Order { DepositAmount = 150 },
        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
        OrderId = orderId, // Same OrderId as main schedule
        CustomerId = customerId,
        Customer = customer
    };

    // Set up the repository mock with specific behavior for different queries
    _customerScheduleRepositoryMock
        .Setup(x => x.FindAll(It.Is<Expression<Func<CustomerSchedule, bool>>>(
            expr => ExpressionMatchesSchedule(expr, customerSchedule.Id))))
        .Returns(new List<CustomerSchedule> { customerSchedule }.AsQueryable().BuildMockDbSet().Object);

    _customerScheduleRepositoryMock
        .Setup(x => x.FindAll(It.Is<Expression<Func<CustomerSchedule, bool>>>(
            expr => ExpressionMatchesRelatedSchedules(expr, orderId, customerSchedule.Id))))
        .Returns(new List<CustomerSchedule> { relatedSchedule }.AsQueryable().BuildMockDbSet().Object);

    // For WorkingSchedule
    var emptyWorkingSchedules = new List<WorkingSchedule>().AsQueryable().BuildMockDbSet();
    _workingScheduleRepositoryMock
        .Setup(x => x.FindAll(It.IsAny<Expression<Func<WorkingSchedule, bool>>?>()))
        .Returns(emptyWorkingSchedules.Object);

    _clinicRepositoryMock
        .Setup(x => x.FindSingleAsync(It.IsAny<Expression<Func<Clinic, bool>>?>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(new Clinic
        {
            Name = "1", Balance = 500, TaxCode = "1", BusinessLicenseUrl = "1", OperatingLicenseUrl = "1",
            OperatingLicenseExpiryDate = DateTimeOffset.Now,
            Address = "1", City = "1", District = "1", Ward = "1", Email = "1", PhoneNumber = "1", IsParent = true,
            BankName = "1", BankAccountNumber = "1"
        });

    _currentUserServiceMock
        .Setup(x => x.ClinicId)
        .Returns(Guid.NewGuid());

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.Equal(150, customerSchedule.Customer.Balance); // 100 (current) + 50 (related) = 150
    _customerScheduleRepositoryMock.Verify(x => x.Update(It.Is<CustomerSchedule>(s => s.Id == customerSchedule.Id)), Times.Once);
    _customerScheduleRepositoryMock.Verify(x => x.Update(It.Is<CustomerSchedule>(s => s.Id == relatedSchedule.Id)), Times.Once);
    _walletTransactionRepositoryMock.Verify(x => x.Add(It.IsAny<WalletTransaction>()), Times.Once);
}

// Helper methods to match the expressions used in the handler
private bool ExpressionMatchesSchedule(Expression<Func<CustomerSchedule, bool>> expr, Guid scheduleId)
{
    var func = expr.Compile();
    var schedule = new CustomerSchedule { Id = scheduleId };
    return func(schedule);
}

private bool ExpressionMatchesRelatedSchedules(Expression<Func<CustomerSchedule, bool>> expr, Guid orderId, Guid currentScheduleId)
{
    var func = expr.Compile();
    // This should match schedules with same OrderId but different Id
    var schedule = new CustomerSchedule 
    { 
        Id = Guid.NewGuid(), // Different ID
        OrderId = orderId,
        Status = Constant.OrderStatus.ORDER_PENDING 
    };
    return func(schedule);
}
}