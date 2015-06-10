namespace Errs.WebUi.Infrastructure.Logging
{
    using System;
    using System.Web;
    using Serilog.Core;
    using Serilog.Events;

    public class ServerNameEnricher : ILogEventEnricher
    {
        public const string HttpServerNamePropertyName = "HttpServerName";

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent == null)
            {
                throw new ArgumentNullException("logEvent");
            }

            if (HttpContext.Current == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(HttpContext.Current.Request.UserHostName))
            {
                return;
            }

            var serverName = Environment.MachineName;
            var httpRequestClientHostnameProperty = new LogEventProperty(HttpServerNamePropertyName,
                new ScalarValue(serverName));
            logEvent.AddPropertyIfAbsent(httpRequestClientHostnameProperty);
        }
    }
}