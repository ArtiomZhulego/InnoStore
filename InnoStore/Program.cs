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

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey(nameof(EmployeeSearchJob));
    q.AddJob<EmployeeSearchJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity($"{nameof(EmployeeSearchJob)}-trigger")
        .WithCronSchedule("0 0 0 * * ?"));
});

builder.Services.AddLogging();

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<InnoStoreContext>();
    dbContext.Database.Migrate(); 
}

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.Run();