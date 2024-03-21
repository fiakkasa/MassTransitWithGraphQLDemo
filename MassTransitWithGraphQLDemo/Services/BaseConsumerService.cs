using HotChocolate.Subscriptions;
using MassTransit;

namespace MassTransitWithGraphQLDemo.Services;

public abstract class BaseConsumerService<T>(
    ITopicEventSender topicEventSender,
    ILogger logger
) : IConsumer<T>
where T : class
{
    private readonly string _typeName = typeof(T).Name;

    public virtual async Task Consume(ConsumeContext<T> context)
    {
        logger.LogInformation("Received type '{Type}'!", _typeName);

        // send a notification to a specific topic using GraphQL subscriptions
        await topicEventSender.SendAsync(
            Consts.TopicPrefix + _typeName, 
            context.Message, 
            context.CancellationToken
        );

        logger.LogInformation("Propagated type '{Type}' to GraphQL!", _typeName);
    }
}
