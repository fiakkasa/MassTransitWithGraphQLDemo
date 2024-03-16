using HotChocolate.Language;
using MassTransitWithGraphQLDemo.Models;

namespace MassTransitWithGraphQLDemo.GraphQL.Subscriptions;

[ExtendObjectType(OperationType.Subscription)]
public class QuotesSubscriptions
{
    // override as needed
    [Topic(Consts.TopicPrefix + nameof(QuoteWithTimestamp))]
    [Subscribe]
    public QuoteWithTimestamp OnQuote([EventMessage] QuoteWithTimestamp item) => item;
}
