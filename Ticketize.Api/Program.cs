using Ticketize.Api;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
          .WriteTo.Console()
          .ReadFrom.Configuration(context.Configuration));

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

Log.Information($"Ticketize API starting in {builder.Environment.EnvironmentName}");

app.UseSerilogRequestLogging();

// Reset database
await app.ResetDatabaseAsync();

app.Run();
