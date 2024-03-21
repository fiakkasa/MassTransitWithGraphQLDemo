using MassTransitWithGraphQLDemo.Interfaces;
using MassTransitWithGraphQLDemo.Models;

namespace MassTransitWithGraphQLDemo.GraphQL.Queries;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class QuotesQueries
{
    public IQueryable<Quote> GetQuotes([Service] IQuotesService quotesService) => quotesService.Quotes;
}
