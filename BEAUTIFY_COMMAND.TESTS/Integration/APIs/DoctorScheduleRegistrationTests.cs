using System.Net;
using System.Net.Http.Json;
using BEAUTIFY_COMMAND.CONTRACT.Services.WorkingSchedules;
using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_COMMAND.TESTS.Fixtures;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Shared;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BEAUTIFY_COMMAND.TESTS.Integration.APIs;

public class DoctorScheduleRegistrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public DoctorScheduleRegistrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Add test services if needed
                services.AddScoped<TestAuthHandler>();
            });
        });
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        
        // Add authentication headers for doctor role
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer test-token");
        _client.DefaultRequestHeaders.Add("X-User-Role", "DOCTOR");
    }

    [Fact]
    public async Task DoctorRegisterSchedule_WithValidRequest_ShouldReturnSuccess()
    {
        // Arrange
        var doctorId = Guid.NewGuid();
        var scheduleIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        
        var request = new Commands.DoctorRegisterScheduleCommand(
            doctorId,
            scheduleIds);

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/working-schedules/doctor/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<Result>();
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task DoctorRegisterSchedule_WithoutAuthorization_ShouldReturnUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var doctorId = Guid.NewGuid();
        var scheduleIds = new List<Guid> { Guid.NewGuid() };
        
        var request = new Commands.DoctorRegisterScheduleCommand(
            doctorId,
            scheduleIds);

        // Act
        var response = await client.PostAsJsonAsync("/api/v1/working-schedules/doctor/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DoctorRegisterSchedule_WithWrongRole_ShouldReturnForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer test-token");
        client.DefaultRequestHeaders.Add("X-User-Role", "CLINIC_STAFF"); // Not a doctor
        
        var doctorId = Guid.NewGuid();
        var scheduleIds = new List<Guid> { Guid.NewGuid() };
        
        var request = new Commands.DoctorRegisterScheduleCommand(
            doctorId,
            scheduleIds);

        // Act
        var response = await client.PostAsJsonAsync("/api/v1/working-schedules/doctor/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task DoctorRegisterSchedule_ExceedingWeeklyHours_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        var doctorId = Guid.NewGuid();
        var scheduleIds = new List<Guid>();
        
        // Add many schedule IDs to simulate exceeding 44 hours
        for (int i = 0; i < 10; i++)
        {
            scheduleIds.Add(Guid.NewGuid());
        }
        
        var request = new Commands.DoctorRegisterScheduleCommand(
            doctorId,
            scheduleIds);

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/working-schedules/doctor/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<Result>();
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("422", result.Error.Code);
        Assert.Contains("44 hours", result.Error.Message);
    }
}
