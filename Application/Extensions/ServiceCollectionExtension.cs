using Application.Abstractions.FileAggregate;
using Application.Abstractions.OrderAggregate;
using Application.Abstractions.ProductAggregate;
using Application.Abstractions.ProductGroupAggregate;
using Application.Abstractions.Services;
using Application.Abstractions.TransactionAggregate;
using Application.Abstractions.UserAggregate;
using Application.BackgroundJobs;
using Application.Clients.HRM;
using Application.Services;
using Application.Services.Internal.OrderAudits;
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
            services.AddOrderFlow();
        }

        public void AddQuartzJobs(IConfiguration configuration)
        {
            services.AddEmployeeSearchJob(configuration);
            services.AddPassedEventProcessingJob(configuration);

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }

        private void AddServices()
        {
            services.AddScoped<IPassedEventService, PassedEventService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductGroupService, ProductGroupService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IUserService,  UserService>();
        }

        private void AddOrderFlow()
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IInternalOrderAuditService, InternalInternalOrderAuditService>();
            services.AddScoped<IOrderAuditService, OrderAuditService>();
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
