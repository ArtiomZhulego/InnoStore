using InnoStore.CampusInegration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Exceptions;
using Presentation.Handlers;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.Kafka;

namespace InnoStore.Extensions;

internal static class CampusConfigurationExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder ConfigureCampusHandler()
        {
            builder.ConfigureKafkaCampusConfiguration();
            var configuration = builder.GetKafkaCampusConfiguration();

            builder.Host.UseWolverine(options =>
            {
                options.Discovery.IncludeAssembly(typeof(PassedEventHandler).Assembly);

                options.UseKafka(configuration.Server)
                    .ConfigureConsumers(config =>
                    {
                        config.GroupId = configuration.GroupId;
                    });

                var campusMessageSerializer = new CampusMessageSerializer();

                options.ListenToKafkaTopic(configuration.EventsTopic)
                    .DefaultSerializer(campusMessageSerializer)
                    .DefaultIncomingMessage<string>()
                    .ReceiveRawJson<string>()
                    .AddStickyHandler(typeof(PassedEventHandler));

                options.Policies
                    .OnException<PassedEventException>()
                    .RetryOnce()
                    .Then
                    .Discard()
                    .And(async (runtime, context, exception) =>
                    {
                        runtime.Logger.LogInformation($"Sending to {configuration.EventsDlqTopic}.");
                        await context.BroadcastToTopicAsync(configuration.EventsDlqTopic, context.Envelope?.Message!);
                    });
            });

            return builder;
        }

        private void ConfigureKafkaCampusConfiguration()
        {
            builder.Services.Configure<KafkaCampusConfiguration>(builder.Configuration.GetSection(KafkaCampusConfiguration.KafkaSection));
        }

        private KafkaCampusConfiguration GetKafkaCampusConfiguration()
        {
            var configuration = builder.Configuration.GetSection(KafkaCampusConfiguration.KafkaSection)
                    .Get<KafkaCampusConfiguration>()
                    ?? throw new Exception($"Section {KafkaCampusConfiguration.KafkaSection} are not configured properly.");

            return configuration;
        }
    }
}
