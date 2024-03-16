namespace MassTransitWithGraphQLDemo.GraphQL.Queries;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class HelloQueries
{
    public string Hello => "Hello!";
}
