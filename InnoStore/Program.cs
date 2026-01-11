using System.Text.Json.Serialization;
using Application.BackgroundJobs;
using InnoStore.Extensions;
using Presentation.Controllers;
using Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Extensions;
using Quartz;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.ConfigureCors(builder.Configuration);

builder.Services.AddControllers()
                .AddApplicationPart(typeof(HealthController).Assembly)
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration, "DefaultConnection");
builder.Services.AddRepositories();

builder.Services.ConfigureLogger(builder.Configuration);

builder.Services.AddOpenApi();
builder.Services.AddLogging();

builder.Services.AddQuartzJobs(builder.Configuration);

var app = builder.Build();

app.ApplyMigrations();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.Run();