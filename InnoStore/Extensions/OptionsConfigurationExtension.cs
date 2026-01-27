using Application.Abstractions.Options;

namespace InnoStore.Extensions;

public static class OptionsConfigurationExtension
{
    extension (WebApplicationBuilder builder)
    {
        public void ConfigureOptions()
        {
            builder.ConfigurePassedEventProcessingJobOptions();
        }

        private void ConfigurePassedEventProcessingJobOptions()
        {
            builder.Services.AddOptions<PassedEventProcessingJobOptions>()
                .Bind(builder.Configuration.GetSection("Quartz"))
                .Validate(x => x.PassedEventProcessingJobDurationInMilliseconds > 0
                    && x.PassedEventProcessingBatchCount > 0,
                    "Invalid configuration for passed event processing background job.")
                .ValidateOnStart();
        }
    }
}