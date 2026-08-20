var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Logger.LogInformation("SRE Agent Demo App starting up...");

// Root endpoint - just a friendly landing message so hitting the base URL shows something.
app.MapGet("/", () => Results.Ok(new
{
    service = "SRE Agent Demo App - Market Data API",
    status = "running",
    tryThese = new[] { "/health", "/api/marketdata/MSFT" }
}));

// Health check endpoint.
app.MapGet("/health", () => Results.Ok(new
{
    status = "Healthy",
    timestampUtc = DateTime.UtcNow
}));

// The "business" endpoint used for the demo.
// THIS is the endpoint you will intentionally break to simulate a bad production deployment.
app.MapGet("/api/marketdata/{symbol}", (string symbol, ILogger<Program> logger) =>
{
    // ================================================================
    //  DEMO BUG INJECTION POINT
    //
    //   To simulate a bad production deployment during your SRE Agent
    //   demo: delete the two slashes ("//") at the start of the next
    //   line, save the file, then commit and push to GitHub.
    //
    //   To simulate the fix / rollback: put the "//" back and push again.
    // ================================================================
     throw new InvalidOperationException("Simulated outage: market data provider unreachable after latest deployment.");

    var price = GetSimulatedPrice(symbol);
    logger.LogInformation("Served price for {Symbol}: {Price}", symbol.ToUpperInvariant(), price);

    return Results.Ok(new
    {
        symbol = symbol.ToUpperInvariant(),
        price,
        currency = "USD",
        asOfUtc = DateTime.UtcNow
    });
});

app.Run();

// Produces a simple, semi-realistic fake price for the given symbol.
// (Not real market data - this is just enough logic to make the demo feel alive.)
static decimal GetSimulatedPrice(string symbol)
{
    var seed = symbol.ToUpperInvariant().Sum(c => (int)c);
    var basePrice = 50 + (seed % 200);
    var cents = Random.Shared.Next(0, 100);
    return basePrice + (cents / 100m);
}
