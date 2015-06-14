namespace Errs.WebUi.Infrastructure.Logging
{
    using System;
    using Serilog;
    using Serilog.Context;
    using Serilog.Core;

    public class Logger : ILogger
    {
        private IDisposable _disposable;

        public void ForContext(string propertyName, object value, bool destructureObjects = false)
        {
            _disposable = LogContext.PushProperty(propertyName, value, destructureObjects);
        }

        public void ForContext<TSource>()
        {
            ForContext(typeof(TSource));
        }

        public void ForContext(Type source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            _disposable = LogContext.PushProperty(Constants.SourceContextPropertyName, source.FullName);
        }

        public void FlushContext()
        {
            _disposable.Dispose();
        }

        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Log.Verbose(messageTemplate, propertyValues);
        }

        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Verbose(exception, messageTemplate, propertyValues);
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            Log.Debug(messageTemplate, propertyValues);
        }

        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Debug(exception, messageTemplate, propertyValues);
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            Log.Information(messageTemplate, propertyValues);
        }

        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Information(exception, messageTemplate, propertyValues);
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            Log.Warning(messageTemplate, propertyValues);
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Warning(exception, messageTemplate, propertyValues);
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            Log.Error(messageTemplate, propertyValues);
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Error(exception, messageTemplate, propertyValues);
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            Log.Fatal(messageTemplate, propertyValues);
        }

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Fatal(exception, messageTemplate, propertyValues);
        }
    }
}