# MassTransit With GraphQL Demo

Quick demo of using MassTransit and HotChocolate!

📝 Events can be observed via any GraphQL editor that supports web sockets and running request:

```graphql
subscription {
  onQuote {
    text
    author
    timestamp
  }
}

subscription {
  onQuoteByAuthor(author: "Lao Tzu") {
    text
    author
    timestamp
  }
}

subscription {
  onQuoteByAuthors(authors: ["Lao Tzu", "Steve Jobs", "Mahatma Gandhi"]) {
    text
    author
    timestamp
  }
}

query {
  quotes {
    author
    text
  }
}
```

## Spinning up the service

- VS Code: use the included profile
- cli: `dotnet run --project ./MassTransitWithGraphQLDemo/MassTransitWithGraphQLDemo.csproj --urls https://localhost:7027`

## Try it out!

- BananaCakePop: https://localhost:7027/graphql/

## References

- MassTransit: https://masstransit.io/documentation/concepts
- HotChocolate: https://chillicream.com/docs/hotchocolate
