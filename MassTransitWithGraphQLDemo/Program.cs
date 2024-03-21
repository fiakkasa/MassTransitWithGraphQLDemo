using MassTransitWithGraphQLDemo.Extensions;
using MassTransitWithGraphQLDemo.Interfaces;
using MassTransitWithGraphQLDemo.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddMassTransitServices();
services.AddGraphQLServices();

services.AddSingleton<IQuotesService, QuotesService>();
services.AddHostedService<QuotesProducerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseWebSockets();
app.MapGraphQL();

app.Run();
