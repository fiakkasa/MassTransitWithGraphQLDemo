using MassTransit;
using MassTransitWithGraphQLDemo.Interfaces;
using MassTransitWithGraphQLDemo.Models;

namespace MassTransitWithGraphQLDemo.Services;

// simulate events coming from an eventing system
public class QuotesProducerService(
    IBus bus,
    IHostApplicationLifetime hostApplicationLifetime,
    IQuotesService quotesService,
    ILogger<QuotesProducerService> logger
) : BackgroundService
{
   

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("The service will wait for the app to start...");

        while (!hostApplicationLifetime.ApplicationStarted.IsCancellationRequested)
        {
            await Task.Delay(500, stoppingToken);
        }

        logger.LogInformation("The service can now start!");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // simulate randomness
                await Task.Delay(Random.Shared.Next(5000, 6000), stoppingToken);

                var (text, author) = quotesService.GetRandomQuote();

                await bus.Publish(
                    new QuoteWithTimestamp(text, author, DateTimeOffset.Now),
                    stoppingToken
                );
            }
            catch (TaskCanceledException)
            {
                logger.LogInformation("The service is requested to abort...");
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred with message: '{ErrorMessage}'", ex.Message);
            }
        }
    }
}
