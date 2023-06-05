using IndexBlock.Common.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IndexBlock.Common.Extensions
{
    public static class CustomFileLoggerExtensions
    {
        public static ILoggingBuilder AddCustomFileLogger(this ILoggingBuilder builder, Action<CustomLoggerOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, CustomLoggerProvider>();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
