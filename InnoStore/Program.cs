using Application.Extensions;
using InnoStore.Extensions;
using InnoStore.Middlewares;
using Persistence.Extensions;
using Presentation.Extensions;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.ConfigureOptions();

builder.Services.ConfigureCors(builder.Configuration);

builder.Services.AddPresentationControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddInterceptors();
builder.Services.AddInitiaizers();
builder.Services.AddDatabaseManagers();

builder.Services.ConfigureLogger(builder.Configuration);
builder.Services.ConfigureMinio(builder.Configuration);

builder.Services.AddOpenApi();
builder.Services.AddLogging();

builder.Services.AddQuartzJobs(builder.Configuration);

builder.ConfigureCampusHandler();

var app = builder.Build();

await app.ExecuteActionsBeforeStart();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.Run();