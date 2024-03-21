using HotChocolate.Subscriptions;
using MassTransit;
using MassTransitWithGraphQLDemo.Models;

namespace MassTransitWithGraphQLDemo.Services;

public class QuoteByAuthorConsumerService(ITopicEventSender topicEventSender, ILogger<QuoteByAuthorConsumerService> logger)
    : IConsumer<QuoteWithTimestamp>
{
    private readonly string _typeName = nameof(QuoteWithTimestamp);

    public virtual async Task Consume(ConsumeContext<QuoteWithTimestamp> context)
    {
        var author = context.Message.Author;

        logger.LogInformation("Received type '{Type}' by author '{Author}'!", _typeName, author);

        // send a notification to a specific topic using GraphQL subscriptions
        await topicEventSender.SendAsync(
            $"{Consts.TopicPrefix}{_typeName}ByAuthor:{author}",
            context.Message,
            context.CancellationToken
        );

        logger.LogInformation("Propagated type '{Type}' by author '{Author}' to GraphQL!", _typeName, author);
    }
}
