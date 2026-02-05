using Application.Abstractions.FileAggregate;
using Application.Abstractions.ProductAggregate;
using Application.Abstractions.ProductGroupAggregate;
using Application.Abstractions.Services;
using Application.BackgroundJobs;
using Application.Clients.HRM;
using Application.Services;
using Domain.Abstractions;
using FluentValidation;
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
            services.AddEmployeeSearchJob(configuration);
            services.AddPassedEventProcessingJob(configuration);

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }

        public void AddValidators()
        {
            services.AddValidatorsFromAssemblyContaining<AssemblyMarker>();
        }

        private void AddServices()
        {
            services.AddTransient<IPassedEventService, PassedEventService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductGroupService, ProductGroupService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
        }

        private void AddEmployeeSearchJob(IConfiguration configuration)
        {
            services.AddQuartz(q =>
            {
                var jobKey = new JobKey(nameof(EmployeeSearchJob));
                q.AddJob<EmployeeSearchJob>(opts => opts.WithIdentity(jobKey));

                var cron = configuration["Quartz:EmployeeSearchJobCron"];

                if (string.IsNullOrWhiteSpace(cron))
                {
                    throw new InvalidOperationException("Quartz cron expression is not configured.");
                }

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity($"{nameof(EmployeeSearchJob)}-trigger")
                    .WithCronSchedule(cron));
            });
        }

        private void AddPassedEventProcessingJob(IConfiguration configuration)
        {
            services.AddQuartz(q =>
            {
                var jobKey = new JobKey(nameof(PassedEventProcessingJob));
                q.AddJob<PassedEventProcessingJob>(opts => opts.WithIdentity(jobKey));

                var cron = configuration["Quartz:PassedEventProcessingJobCron"];

                if (string.IsNullOrWhiteSpace(cron))
                {
                    throw new InvalidOperationException("Quartz cron expression is not configured.");
                }

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity($"{nameof(PassedEventProcessingJob)}-trigger")
                    .WithCronSchedule(cron));
            });
        }
    }
}
