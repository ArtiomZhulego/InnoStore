using InnoStore.CampusInegration;
using Presentation.Exceptions;
using Presentation.Handlers;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.Kafka;

namespace InnoStore.Extensions;

internal static class WebApplicationBuilderExtension
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder ConfigureCampusHandler()
        {
            var configuration = GetKafkaCampusConfiguration(builder);

            builder.Host.UseWolverine(options =>
            {
                options.Discovery.IncludeAssembly(typeof(PassedEventHandler).Assembly);

                options.UseKafka(configuration.Server)
                    .ConfigureConsumers(config =>
                    {
                        config.GroupId = configuration.GroupId;
                    });

                var campusMessageSerializer = new CampusMessageSerializer();

                options.ListenToKafkaTopic(configuration.PassedEventTopic)
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
                        runtime.Logger.LogInformation($"Sending to {configuration.DeadLetterQueueTopic}.");
                        await context.BroadcastToTopicAsync(configuration.DeadLetterQueueTopic, context.Envelope?.Message!);
                    });
            });

            return builder;
        }
    }

    private static KafkaCampusConfiguration GetKafkaCampusConfiguration(WebApplicationBuilder builder)
    {
        var server = GetEnvironmentVariable(builder, "KAFKA_SERVER");
        var groupId = GetEnvironmentVariable(builder, "KAFKA_GROUP_ID");
        var passedEventTopic = GetEnvironmentVariable(builder, "KAFKA_EVENTS_TOPIC");
        var deadLetterQueueTopic = GetEnvironmentVariable(builder, "KAFKA_EVENTS_DLQ_TOPIC");

        var configuration = new KafkaCampusConfiguration
        {
            Server = server,
            GroupId = groupId,
            PassedEventTopic = passedEventTopic,
            DeadLetterQueueTopic = deadLetterQueueTopic,
        };

        return configuration;
    }

    private static string GetEnvironmentVariable(WebApplicationBuilder builder, string environmentVariableName)
    {
        var value = builder.Configuration[environmentVariableName];

        if (string.IsNullOrEmpty(value))
        {
            throw new NullReferenceException($"Environment variable ${environmentVariableName} is not set.");
        }

        return value;
    }
}
