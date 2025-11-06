using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Logging
{
    public static class FileLoggerExtensions
    {
        public static ILoggingBuilder AddFileLogger(
            this ILoggingBuilder builder,
            Action<FileLoggerConfiguration> configure)
        {
            var config = new FileLoggerConfiguration();
            configure(config);

            builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>(
                services => new FileLoggerProvider(config));

            return builder;
        }
    }
}
