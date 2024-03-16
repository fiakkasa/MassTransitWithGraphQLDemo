using HotChocolate.Subscriptions;
using MassTransitWithGraphQLDemo.Models;

namespace MassTransitWithGraphQLDemo.Services;

public class QuoteConsumerService(ITopicEventSender topicEventSender, ILogger<QuoteConsumerService> logger) 
    : BaseConsumerService<QuoteWithTimestamp>(topicEventSender, logger) { }
