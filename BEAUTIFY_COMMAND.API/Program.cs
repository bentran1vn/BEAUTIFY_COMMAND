using BEAUTIFY_COMMAND.API.DependencyInjection.Extensions;
using BEAUTIFY_COMMAND.API.Middlewares;
using BEAUTIFY_COMMAND.APPLICATION.DependencyInjection.Extensions;
using BEAUTIFY_COMMAND.APPLICATION.Hub;
using BEAUTIFY_COMMAND.INFRASTRUCTURE.DependencyInjection.Extensions;
using BEAUTIFY_COMMAND.PERSISTENCE.DependencyInjection.Extensions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.API.DependencyInjection.Extensions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Abstractions.Repositories;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.PERSISTENCE.DependencyInjection.Options;
using Carter;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging
    .ClearProviders()
    .AddSerilog();

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Console());

// Add Carter module
builder.Services.AddCarter();

builder.Services
    .AddSwaggerGenNewtonsoftSupport()
    .AddFluentValidationRulesToSwagger()
    .AddEndpointsApiExplorer()
    .AddSwaggerAPI1();

builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.ConfigureCors();

// API Layer
builder.Services.AddJwtAuthenticationAPI1(builder.Configuration);
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddHttpContextAccessor();

// Application Layer
builder.Services.AddMediatRApplication();
builder.Services.AddAutoMapperApplication();

// Persistence Layer
builder.Services.AddInterceptorPersistence();
builder.Services.ConfigureSqlServerRetryOptionsPersistence(
    builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));
builder.Services.AddSqlServerPersistence();
builder.Services.AddRepositoryPersistence();

// Infrastructure Layer
builder.Services.AddServicesInfrastructure();
builder.Services.AddRedisInfrastructure(builder.Configuration);
builder.Services.AddMasstransitRabbitMQInfrastructure(builder.Configuration);
builder.Services.AddQuartzInfrastructure();
builder.Services.AddMediatRInfrastructure();
builder.Services.ConfigureCloudinaryOptionsInfrastucture(builder.Configuration.GetSection(nameof(CloudinaryOptions)));
builder.Services.ConfigureMailOptionsInfrastucture(builder.Configuration.GetSection(nameof(MailOption)));


// Add Middleware => Remember using middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();
builder.Services.AddAntiforgery(options =>
{
    // Optional: Configure anti-forgery options if needed
    options.HeaderName = "X-CSRF-TOKEN"; // Default header for token validation
});

builder.Services.AddSignalR();

var app = builder.Build();

// Using middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline. 
// if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
app.UseSwaggerAPI(); // => After MapCarter => Show Version

app.UseCors("CorsPolicy");
app.MapHub<PaymentHub>("api/PaymentHub");

// app.UseHttpsRedirection();

app.UseAuthentication(); // Need to be before app.UseAuthorization();
app.UseAuthorization();

app.UseAntiforgery();

// Add API Endpoint with carter module
app.MapCarter();

try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}

namespace BEAUTIFY_COMMAND.API
{
    public abstract partial class Program
    {
    }
}