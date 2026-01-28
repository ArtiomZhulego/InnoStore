using System.Text.Json.Serialization;
using Application.Extensions;
using InnoStore.Extensions;
using InnoStore.Middlewares;
using Persistence.Extensions;
using Presentation;
using Presentation.Controllers;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.ConfigureCors(builder.Configuration);

builder.Services.AddControllers()
                .AddApplicationPart(typeof(AssemblyMarker).Assembly)
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddInterceptors();
builder.Services.AddInitiaizers();
builder.Services.AddValidators();

builder.Services.ConfigureLogger(builder.Configuration);
builder.Services.ConfigureMinio(builder.Configuration);

builder.Services.AddOpenApi();
builder.Services.AddLogging();

builder.Services.AddQuartzJobs(builder.Configuration);

var app = builder.Build();

await app.ApplyMigrations();
await app.ApplyDataInitializers();
await app.ApplyBlobStorageInitialization();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.Run();