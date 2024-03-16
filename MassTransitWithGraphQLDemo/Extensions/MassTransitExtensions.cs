using MassTransit;

namespace MassTransitWithGraphQLDemo.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransitServices(this IServiceCollection services)
    {
        services.AddMassTransit(builder =>
        {
            builder.SetKebabCaseEndpointNameFormatter();

            // automatically add consumers from a specified assembly
            builder.AddConsumers(typeof(Program).Assembly);

            // using the in memory provider for the demo; other providers can be added as well
            builder.UsingInMemory((context, options) => options.ConfigureEndpoints(context));
        });

        services.AddMassTransitHostedService();

        return services;
    }
}
