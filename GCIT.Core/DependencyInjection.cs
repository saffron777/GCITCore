using Gaming.Core.Services;
using GCIT.Core.Data;
using GCIT.Core.Helpers;
using GCIT.Core.Logging;
using GCIT.Core.Services;
using GCIT.Core.Services.Interfaces;
using GCIT.Core.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GCIT.Core
{
    public static class DependencyInjection
    {
        // Registra solo servicios de infraestructura (sin DbContext)
        public static IServiceCollection AddInfrastructureCore(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            // Configurar logging  
            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole(); // Ahora AddConsole estará disponible
                logging.AddDebug();

                // Configuración para FileLogger (si tu extensión existe)
                var fileLogLevelString = configuration["Logging:FileLogger:LogLevel"] ?? "Information";
                var fileLogDirectory = configuration["Logging:FileLogger:LogDirectory"] ?? "Logs";

                if (!Enum.TryParse<LogLevel>(fileLogLevelString, true, out var fileLogLevel))
                    fileLogLevel = LogLevel.Information;

                logging.AddFileLogger(options =>
                {
                    options.LogLevel = fileLogLevel;
                    options.LogDirectory = fileLogDirectory;
                });
            });

            Utils.Initialize(configuration);

            services.AddScoped<ICABServices, CABServices>();
            services.AddScoped<ITransacService, TransacService>();
            // Add other infrastructure services here
            return services;
        }

        // Registra el mapeo DefaultDBContext -> TContext (derivado) y además los servicios
        public static IServiceCollection AddInfrastructureWithContext<TContext>(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<DbContextOptionsBuilder> configureDb)
            where TContext : DefaultDBContext
        {
            services.AddDbContext<DefaultDBContext, TContext>(configureDb);
            return services.AddInfrastructureCore(configuration);
        }
    }
}
