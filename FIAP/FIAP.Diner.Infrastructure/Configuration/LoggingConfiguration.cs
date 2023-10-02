using FIAP.Diner.Infrastructure.Logging;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Json;

namespace FIAP.Diner.Infrastructure.Configuration;

public static class LoggingConfiguration
{
    public static IHostBuilder AddLogger(this IHostBuilder builder)
    {
        builder.UseSerilog((context, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.With(new HttpHeaderEnricher("X-Correlation-Id", "CorrelationId"))
            .WriteTo.Conditional(_ => context.HostingEnvironment.IsDevelopment(), c => c.Console())
            .WriteTo.Conditional(_ => !context.HostingEnvironment.IsDevelopment(), c => c.Console(new JsonFormatter()))
        );

        return builder;
    }
}