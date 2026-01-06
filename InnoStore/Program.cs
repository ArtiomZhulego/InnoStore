using System.Text.Json.Serialization;
using InnoStore.Extensions;
using Presentation.Controllers;
using Application.Extensions;
using Persistence.Extensions;
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

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration, "DefaultConnection");

builder.Services.ConfigureLogger(builder.Configuration);

builder.Services.AddOpenApi();


var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.Run();