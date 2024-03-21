using HotChocolate.Language;
using HotChocolate.Subscriptions;
using MassTransitWithGraphQLDemo.Models;

namespace MassTransitWithGraphQLDemo.GraphQL.Subscriptions;

[ExtendObjectType(OperationType.Subscription)]
public class QuotesSubscriptions
{
    // override as needed
    [Topic(Consts.TopicPrefix + nameof(QuoteWithTimestamp))]
    [Subscribe]
    public QuoteWithTimestamp OnQuote([EventMessage] QuoteWithTimestamp item) => item;

    // override as needed
    [Topic($"{Consts.TopicPrefix}{nameof(QuoteWithTimestamp)}ByAuthor:{{{nameof(author)}}}")]
    [Subscribe]
    public QuoteWithTimestamp OnQuoteByAuthor(string author, [EventMessage] QuoteWithTimestamp item) => item;

    // override as needed
    [Topic(Consts.TopicPrefix + nameof(QuoteWithTimestamp))]
    [Subscribe(MessageType = typeof(QuoteWithTimestamp))]
    public async ValueTask<QuoteWithTimestamp?> OnQuoteByAuthors(
        string[] authors,
        [Service] ITopicEventReceiver receiver,
        CancellationToken cancellationToken
    )
    {
        var stream = await receiver.SubscribeAsync<QuoteWithTimestamp>(
            Consts.TopicPrefix + nameof(QuoteWithTimestamp),
            cancellationToken
        );

       return await stream.ReadEventsAsync().FirstOrDefaultAsync(x => authors.Contains(x.Author), cancellationToken);
    }
}
