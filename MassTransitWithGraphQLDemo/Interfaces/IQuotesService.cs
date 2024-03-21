using MassTransitWithGraphQLDemo.Models;

namespace MassTransitWithGraphQLDemo.Interfaces;

public interface IQuotesService
{
    public IQueryable<Quote> Quotes { get; }

    public Quote GetRandomQuote();
}
