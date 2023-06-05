using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IndexBlock.Common.Logger
{

    public class CustomLoggerProvider : ILoggerProvider
    {
        public readonly CustomLoggerOptions _customLoggerOptions;

        public CustomLoggerProvider(IOptions<CustomLoggerOptions> customLoggerOptions)
        {
            _customLoggerOptions = customLoggerOptions.Value;

            if (!Directory.Exists(_customLoggerOptions.FolderPath))
            {
                Directory.CreateDirectory(_customLoggerOptions.FolderPath);
            }
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new CustomFileLogger(this);
        }

        public void Dispose()
        {
        }
    }
}
