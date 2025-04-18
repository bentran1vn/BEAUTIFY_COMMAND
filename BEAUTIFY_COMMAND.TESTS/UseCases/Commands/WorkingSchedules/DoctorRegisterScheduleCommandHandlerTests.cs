using BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.WorkingSchedules;
using BEAUTIFY_COMMAND.CONTRACT.Services.WorkingSchedules;
using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_COMMAND.DOMAIN.Exceptions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BEAUTIFY_COMMAND.TESTS.UseCases.Commands.WorkingSchedules;

public class DoctorRegisterScheduleCommandHandlerTests
{
    private readonly Mock<IRepositoryBase<Staff, Guid>> _mockStaffRepository;
    private readonly Mock<IRepositoryBase<WorkingSchedule, Guid>> _mockWorkingScheduleRepository;
    private readonly Mock<IRepositoryBase<UserClinic, Guid>> _mockUserClinicRepository;
    private readonly DoctorRegisterScheduleCommandHandler _handler;

    public DoctorRegisterScheduleCommandHandlerTests()
    {
        _mockStaffRepository = new Mock<IRepositoryBase<Staff, Guid>>();
        _mockWorkingScheduleRepository = new Mock<IRepositoryBase<WorkingSchedule, Guid>>();
        _mockUserClinicRepository = new Mock<IRepositoryBase<UserClinic, Guid>>();
        _handler = new DoctorRegisterScheduleCommandHandler(
            _mockStaffRepository.Object,
            _mockWorkingScheduleRepository.Object,
            _mockUserClinicRepository.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ShouldRegisterSchedules()
    {
        // Arrange
        var doctorId = Guid.NewGuid();
        var clinicId = Guid.NewGuid();
        var userClinicId = Guid.NewGuid();
        var scheduleId1 = Guid.NewGuid();
        var scheduleId2 = Guid.NewGuid();

        var doctor = new Staff
        {
            Id = doctorId,
            FirstName = "John",
            LastName = "Doe",
            Role = new Role { Name = Constant.Role.DOCTOR }
        };

        var userClinic = new UserClinic
        {
            Id = userClinicId,
            UserId = doctorId,
            ClinicId = clinicId
        };

        var monday = DateOnly.FromDateTime(DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1)); // Get next Monday
        var schedules = new List<WorkingSchedule>
        {
            new()
            {
                Id = scheduleId1,
                Date = monday,
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(12, 0, 0),
                DoctorClinicId = null // Empty slot
            },
            new()
            {
                Id = scheduleId2,
                Date = monday.AddDays(1), // Tuesday
                StartTime = new TimeSpan(13, 0, 0),
                EndTime = new TimeSpan(17, 0, 0),
                DoctorClinicId = null // Empty slot
            }
        };

        var command = new Commands.DoctorRegisterScheduleCommand(
            doctorId,
            new List<Guid> { scheduleId1, scheduleId2 });

        _mockStaffRepository.Setup(x => x.FindByIdAsync(doctorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(doctor);

        _mockUserClinicRepository.Setup(x => x.FindSingleAsync(It.IsAny<Func<UserClinic, bool>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(userClinic);

        var mockDbSet = new Mock<DbSet<WorkingSchedule>>();
        mockDbSet.As<IQueryable<WorkingSchedule>>().Setup(m => m.Provider).Returns(schedules.AsQueryable().Provider);
        mockDbSet.As<IQueryable<WorkingSchedule>>().Setup(m => m.Expression).Returns(schedules.AsQueryable().Expression);
        mockDbSet.As<IQueryable<WorkingSchedule>>().Setup(m => m.ElementType).Returns(schedules.AsQueryable().ElementType);
        mockDbSet.As<IQueryable<WorkingSchedule>>().Setup(m => m.GetEnumerator()).Returns(schedules.AsQueryable().GetEnumerator());

        _mockWorkingScheduleRepository.Setup(x => x.FindAll(It.IsAny<Func<WorkingSchedule, bool>>()))
            .Returns(schedules.AsQueryable());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        _mockWorkingScheduleRepository.Verify(x => x.Update(It.Is<WorkingSchedule>(
            s => s.DoctorClinicId == userClinicId)), Times.Exactly(2));
        
        // Verify that the domain event was raised
        var schedule = schedules.First();
        var domainEvent = schedule.GetDomainEvents().FirstOrDefault() as BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.WorkingSchedules.DomainEvents.DoctorScheduleRegistered;
        Assert.NotNull(domainEvent);
        Assert.Equal(doctorId, domainEvent.DoctorId);
        Assert.Equal("John Doe", domainEvent.DoctorName);
        Assert.Equal(2, domainEvent.WorkingScheduleEntities.Count);
    }

    [Fact]
    public async Task Handle_WithInvalidDoctor_ShouldReturnFailure()
    {
        // Arrange
        var doctorId = Guid.NewGuid();
        var scheduleId = Guid.NewGuid();

        var command = new Commands.DoctorRegisterScheduleCommand(
            doctorId,
            new List<Guid> { scheduleId });

        _mockStaffRepository.Setup(x => x.FindByIdAsync(doctorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Staff)null);

        // Act & Assert
        await Assert.ThrowsAsync<UserException.UserNotFoundException>(() => 
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithNonDoctorRole_ShouldReturnFailure()
    {
        // Arrange
        var doctorId = Guid.NewGuid();
        var scheduleId = Guid.NewGuid();

        var staff = new Staff
        {
            Id = doctorId,
            FirstName = "John",
            LastName = "Doe",
            Role = new Role { Name = "STAFF" } // Not a doctor
        };

        var command = new Commands.DoctorRegisterScheduleCommand(
            doctorId,
            new List<Guid> { scheduleId });

        _mockStaffRepository.Setup(x => x.FindByIdAsync(doctorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(staff);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("403", result.Error.Code);
    }

    [Fact]
    public async Task Handle_WithNoClinicAssociation_ShouldReturnFailure()
    {
        // Arrange
        var doctorId = Guid.NewGuid();
        var scheduleId = Guid.NewGuid();

        var doctor = new Staff
        {
            Id = doctorId,
            FirstName = "John",
            LastName = "Doe",
            Role = new Role { Name = Constant.Role.DOCTOR }
        };

        var command = new Commands.DoctorRegisterScheduleCommand(
            doctorId,
            new List<Guid> { scheduleId });

        _mockStaffRepository.Setup(x => x.FindByIdAsync(doctorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(doctor);

        _mockUserClinicRepository.Setup(x => x.FindSingleAsync(It.IsAny<Func<UserClinic, bool>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserClinic)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("404", result.Error.Code);
    }

    [Fact]
    public async Task Handle_WithScheduleNotFound_ShouldReturnFailure()
    {
        // Arrange
        var doctorId = Guid.NewGuid();
        var clinicId = Guid.NewGuid();
        var userClinicId = Guid.NewGuid();
        var scheduleId = Guid.NewGuid();

        var doctor = new Staff
        {
            Id = doctorId,
            FirstName = "John",
            LastName = "Doe",
            Role = new Role { Name = Constant.Role.DOCTOR }
        };

        var userClinic = new UserClinic
        {
            Id = userClinicId,
            UserId = doctorId,
            ClinicId = clinicId
        };

        var command = new Commands.DoctorRegisterScheduleCommand(
            doctorId,
            new List<Guid> { scheduleId });

        _mockStaffRepository.Setup(x => x.FindByIdAsync(doctorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(doctor);

        _mockUserClinicRepository.Setup(x => x.FindSingleAsync(It.IsAny<Func<UserClinic, bool>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(userClinic);

        _mockWorkingScheduleRepository.Setup(x => x.FindAll(It.IsAny<Func<WorkingSchedule, bool>>()))
            .Returns(new List<WorkingSchedule>().AsQueryable());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("404", result.Error.Code);
    }

    [Fact]
    public async Task Handle_WithAlreadyAssignedSchedule_ShouldReturnFailure()
    {
        // Arrange
        var doctorId = Guid.NewGuid();
        var clinicId = Guid.NewGuid();
        var userClinicId = Guid.NewGuid();
        var otherDoctorClinicId = Guid.NewGuid();
        var scheduleId = Guid.NewGuid();

        var doctor = new Staff
        {
            Id = doctorId,
            FirstName = "John",
            LastName = "Doe",
            Role = new Role { Name = Constant.Role.DOCTOR }
        };

        var userClinic = new UserClinic
        {
            Id = userClinicId,
            UserId = doctorId,
            ClinicId = clinicId
        };

        var schedules = new List<WorkingSchedule>
        {
            new()
            {
                Id = scheduleId,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(12, 0, 0),
                DoctorClinicId = otherDoctorClinicId // Already assigned to another doctor
            }
        };

        var command = new Commands.DoctorRegisterScheduleCommand(
            doctorId,
            new List<Guid> { scheduleId });

        _mockStaffRepository.Setup(x => x.FindByIdAsync(doctorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(doctor);

        _mockUserClinicRepository.Setup(x => x.FindSingleAsync(It.IsAny<Func<UserClinic, bool>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(userClinic);

        _mockWorkingScheduleRepository.Setup(x => x.FindAll(It.IsAny<Func<WorkingSchedule, bool>>()))
            .Returns(schedules.AsQueryable());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("409", result.Error.Code);
    }

    [Fact]
    public async Task Handle_ExceedingWeeklyHourLimit_ShouldReturnFailure()
    {
        // Arrange
        var doctorId = Guid.NewGuid();
        var clinicId = Guid.NewGuid();
        var userClinicId = Guid.NewGuid();
        var scheduleId1 = Guid.NewGuid();
        var scheduleId2 = Guid.NewGuid();
        var existingScheduleId = Guid.NewGuid();

        var doctor = new Staff
        {
            Id = doctorId,
            FirstName = "John",
            LastName = "Doe",
            Role = new Role { Name = Constant.Role.DOCTOR }
        };

        var userClinic = new UserClinic
        {
            Id = userClinicId,
            UserId = doctorId,
            ClinicId = clinicId
        };

        var monday = DateOnly.FromDateTime(DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1)); // Get next Monday
        
        // New schedules to register (20 hours)
        var newSchedules = new List<WorkingSchedule>
        {
            new()
            {
                Id = scheduleId1,
                Date = monday,
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(17, 0, 0), // 8 hours
                DoctorClinicId = null
            },
            new()
            {
                Id = scheduleId2,
                Date = monday.AddDays(1), // Tuesday
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(21, 0, 0), // 12 hours
                DoctorClinicId = null
            }
        };
        
        // Existing schedules (30 hours)
        var existingSchedules = new List<WorkingSchedule>
        {
            new()
            {
                Id = existingScheduleId,
                Date = monday.AddDays(2), // Wednesday
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(20, 0, 0), // 12 hours
                DoctorClinicId = userClinicId
            },
            new()
            {
                Id = Guid.NewGuid(),
                Date = monday.AddDays(3), // Thursday
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(20, 0, 0), // 12 hours
                DoctorClinicId = userClinicId
            },
            new()
            {
                Id = Guid.NewGuid(),
                Date = monday.AddDays(4), // Friday
                StartTime = new TimeSpan(14, 0, 0),
                EndTime = new TimeSpan(20, 0, 0), // 6 hours
                DoctorClinicId = userClinicId
            }
        };

        var command = new Commands.DoctorRegisterScheduleCommand(
            doctorId,
            new List<Guid> { scheduleId1, scheduleId2 });

        _mockStaffRepository.Setup(x => x.FindByIdAsync(doctorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(doctor);

        _mockUserClinicRepository.Setup(x => x.FindSingleAsync(It.IsAny<Func<UserClinic, bool>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(userClinic);

        _mockWorkingScheduleRepository.Setup(x => x.FindAll(It.Is<Func<WorkingSchedule, bool>>(
            f => f.Method.Name.Contains("Contains"))))
            .Returns(newSchedules.AsQueryable());
            
        _mockWorkingScheduleRepository.Setup(x => x.FindAll(It.Is<Func<WorkingSchedule, bool>>(
            f => !f.Method.Name.Contains("Contains"))))
            .Returns(existingSchedules.AsQueryable());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("422", result.Error.Code);
        Assert.Contains("44 hours", result.Error.Message);
    }
}
