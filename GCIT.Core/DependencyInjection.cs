
using Gaming.Core.Services;
using GCIT.Core.Data;
using GCIT.Core.Services;
using GCIT.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GCIT.Core
{
    public static class DependencyInjection
    {
        // Registra solo servicios de infraestructura (sin DbContext)
        public static IServiceCollection AddInfrastructureCore(this IServiceCollection services)
        {
            
            services.AddScoped<ICABServices, CABServices>();
            services.AddScoped<ITransacService, TransacService>();
            // Add other infrastructure services here
            return services;
        }

        // Registra el mapeo DefaultDBContext -> TContext (derivado) y además los servicios
        public static IServiceCollection AddInfrastructureWithContext<TContext>(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> configureDb)
            where TContext : DefaultDBContext
        {
            services.AddDbContext<DefaultDBContext, TContext>(configureDb);
            return services.AddInfrastructureCore();
        }
    }
}
