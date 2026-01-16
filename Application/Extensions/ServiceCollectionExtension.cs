using Application.Abstractions.Services;
using Application.BackgroundJobs;
using Application.Clients.HRM;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Application.Extensions;

public static class ServiceCollectionExtension
{
    extension(IServiceCollection services)
    {
        public void AddApplicationServices(IConfiguration configuration)
        {
            services.AddHrm(configuration);
            services.AddServices();
        }

        public void AddQuartzJobs(IConfiguration configuration)
        {
            services.AddQuartz(q =>
            {
                var jobKey = new JobKey(nameof(EmployeeSearchJob));
                q.AddJob<EmployeeSearchJob>(opts => opts.WithIdentity(jobKey));

                var cron = configuration["Quartz:EmployeeSearchJobCron"];
                if (string.IsNullOrWhiteSpace(cron))
                    throw new InvalidOperationException("Quartz cron expression is not configured.");

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity($"{nameof(EmployeeSearchJob)}-trigger")
                    .WithCronSchedule(cron));
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }

        private void AddServices()
        {
            services.AddTransient<IPassedEventService, PassedEventService>();
        }
    }
}
