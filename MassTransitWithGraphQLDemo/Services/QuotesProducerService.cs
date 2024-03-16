using MassTransit;
using MassTransitWithGraphQLDemo.Models;

namespace MassTransitWithGraphQLDemo.Services;

// simulate events coming from an eventing system
public class QuotesProducerService(
    IBus bus,
    IHostApplicationLifetime hostApplicationLifetime,
    ILogger<QuotesProducerService> logger
) : BackgroundService
{
    private static readonly Quote[] _quotes =
    [
        new ("The only way to do great work is to love what you do.", "Steve Jobs"),
        new ("In the end, it's not the years in your life that count. It's the life in your years.", "Abraham Lincoln"),
        new ("Be the change that you wish to see in the world.", "Mahatma Gandhi"),
        new ("The future belongs to those who believe in the beauty of their dreams.", "Eleanor Roosevelt"),
        new ("The only limit to our realization of tomorrow will be our doubts of today.", "Franklin D. Roosevelt"),
        new ("The journey of a thousand miles begins with one step.", "Lao Tzu"),
        new ("Two things are infinite: the universe and human stupidity; and I'm not sure about the universe.", "Albert Einstein"),
        new ("Success is not final, failure is not fatal: It is the courage to continue that counts.", "Winston Churchill"),
        new ("Believe you can and you're halfway there.", "Theodore Roosevelt"),
        new ("The only true wisdom is in knowing you know nothing.", "Socrates")
    ];

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

                var (text, author) = _quotes[Random.Shared.Next(0, _quotes.Length - 1)];

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
