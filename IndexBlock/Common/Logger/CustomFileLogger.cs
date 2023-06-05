using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace IndexBlock.Common.Logger
{
    internal class CustomFileLogger : ILogger
    {
        protected readonly CustomLoggerProvider _customLoggerProvider;

        public CustomFileLogger([NotNull] CustomLoggerProvider customLoggerProvider)
        {
            _customLoggerProvider = customLoggerProvider;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var fullFilePath = _customLoggerProvider._customLoggerOptions.FolderPath + "/" + _customLoggerProvider._customLoggerOptions.FilePath.Replace("{date}", DateTimeOffset.Now.ToString("yyyyMMdd"));
            var logRecord = string.Format("{0} [{1}] {2} {3}", "[" + DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss+00:00") + "]", logLevel.ToString(), formatter(state, exception), exception != null ? exception.StackTrace : "");

            using (var streamWriter = new StreamWriter(fullFilePath, true))
            {
                streamWriter.WriteLine(logRecord);
            }
        }
    }
}
