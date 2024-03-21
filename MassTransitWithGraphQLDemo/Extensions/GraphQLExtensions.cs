using MassTransitWithGraphQLDemo.GraphQL.Queries;
using MassTransitWithGraphQLDemo.GraphQL.Subscriptions;

namespace MassTransitWithGraphQLDemo.Extensions;

public static class GraphQLExtensions
{
    public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddInMemorySubscriptions()
            .AddQueryType()
            .AddSubscriptionType()
            .AddTypeExtension<HelloQueries>()
            .AddTypeExtension<QuotesQueries>()
            .AddTypeExtension<QuotesSubscriptions>();

        return services;
    }
}
