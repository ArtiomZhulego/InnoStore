using Application.BackgroundJobs;
using Application.Extensions;
using ImTools;
using InnoStore.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Extensions;
using Presentation.Controllers;
using Presentation.Handlers;
using Quartz;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.Kafka;

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

builder.Host.UseWolverine(options =>
{
    options.Discovery.IncludeAssembly(typeof(CampusHandler).Assembly);

    options.UseKafka("localhost:9094")
        .ConfigureConsumers(config =>
        {
            config.GroupId = "innostore";
        });

    options.ListenToKafkaTopic("events")
        .ReceiveRawJson<PassedEvent>();

    options.Policies
        .OnException<Exception>()
        .RetryOnce()
        .Then
        .Discard()
        .And(async (r, context, ex) =>
        {
            var dlq = $"events.dlq";
            r.Logger.LogInformation($"Sending to DLQ: {dlq}.");
            await context.BroadcastToTopicAsync(dlq, context.Envelope.Message);
        });
});

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