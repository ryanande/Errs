namespace Errs.WebUi
{
    using Infrastructure.Logging;
    using Serilog;
    using SerilogWeb.Classic;
    using SerilogWeb.Classic.Enrichers;

    public class LogConfig
    {
        private readonly IConfigurations _configurations;
        private readonly IConnectionStrings _connectionStrings;

        public LogConfig(IConnectionStrings connectionStrings, IConfigurations configurations)
        {
            _connectionStrings = connectionStrings;
            _configurations = configurations;
        }

        public void Initialize()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Trace(outputTemplate: "{Timestamp} [{Level}] ({HttpRequestId}|{UserName}) {Message}{NewLine}{Exception}")
                .WriteTo.MSSqlServer(_connectionStrings.LoggingDb, _configurations.LogTableName)
                .Enrich.With<HttpRequestIdEnricher>()
                .Enrich.With<HttpSessionIdEnricher>()
                .Enrich.With<HttpRequestUrlEnricher>()
                .Enrich.With<HttpRequestTypeEnricher>()
                .Enrich.With<ServerNameEnricher>()
                .Enrich.With(new UserNameEnricher("anonymous", System.Environment.UserName))
                .ReadFrom.AppSettings()
                .Enrich.FromLogContext()
                .CreateLogger();

            ApplicationLifecycleModule.DebugLogPostedFormData = true;
        }
    }
}