using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace FIAP.Diner.Infrastructure.Logging;

public class HttpHeaderEnricher : ILogEventEnricher
{
    private readonly string _propertyName;
    private readonly string _headerKey;
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpHeaderEnricher(string headerKey, string propertyName) : this(headerKey, propertyName, new HttpContextAccessor())
    {
    }

    private HttpHeaderEnricher(string headerKey, string propertyName, IHttpContextAccessor contextAccessor)
    {
        _headerKey = headerKey;
        _propertyName = propertyName;
        _contextAccessor = contextAccessor;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (_contextAccessor.HttpContext == null)
            return;

        var headerValue = GetHeaderValue();

        if (headerValue is null)
            return;

        var headerValueProperty = new LogEventProperty(_propertyName, new ScalarValue(headerValue));
        logEvent.AddOrUpdateProperty(headerValueProperty);
    }

    private string? GetHeaderValue()
    {
        var header = string.Empty;

        if (_contextAccessor.HttpContext!.Request.Headers.TryGetValue(_headerKey, out var values))
        {
            header = values.FirstOrDefault();
        }
        else if (_contextAccessor.HttpContext.Response.Headers.TryGetValue(_headerKey, out values))
        {
            header = values.FirstOrDefault();
        }

        return header;
    }
}
