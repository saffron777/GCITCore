using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Logging
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly FileLoggerConfiguration _config;
        private bool _disposed = false;

        public FileLoggerProvider(FileLoggerConfiguration config)
        {
            _config = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(FileLoggerProvider));

            return new FileLogger(categoryName, _config);
        }

        public void Dispose()
        {
            _disposed = true;
        }
    }
}
