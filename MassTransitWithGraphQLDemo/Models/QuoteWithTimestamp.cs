namespace MassTransitWithGraphQLDemo.Models;

public record QuoteWithTimestamp(string Text, string Author, DateTimeOffset Timestamp) : Quote(Text, Author);
