using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Logging
{
    public class FileLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly FileLoggerConfiguration _config;

        public FileLogger(string categoryName, FileLoggerConfiguration config)
        {
            _categoryName = categoryName;
            _config = config;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel >= _config.LogLevel;

        public void Log<TState>(
           LogLevel logLevel,
           EventId eventId,
           TState state,
           Exception exception,
           Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            var message = formatter(state, exception);
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{logLevel}] [{_categoryName}] {message}";

            if (exception != null)
            {
                logEntry += $"\n{exception}";
            }

            WriteToFile(logEntry);
        }

        private void WriteToFile(string message)
        {
            try
            {
                var logFilePath = Path.Combine(
                    _config.LogDirectory,
                    $"{DateTime.Now:yyyyMMdd}.log");

                // Crear directorio si no existe
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

                // Escribir en el archivo
                File.AppendAllText(logFilePath, message + Environment.NewLine, Encoding.UTF8);
            }
            catch
            {
                // Fallback en caso de error de escritura
                Console.WriteLine($"Failed to write log: {message}");
            }
        }
    }
}
