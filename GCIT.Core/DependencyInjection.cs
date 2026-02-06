using Gaming.Core.Services;
using GCIT.Core.Data;
using GCIT.Core.Helpers;
using GCIT.Core.Logging;
using GCIT.Core.Services;
using GCIT.Core.Services.Interfaces;
using GCIT.Core.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

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

            
            // CORS policy llamada "acceder"
            services.AddCors(options =>
            {
                options.AddPolicy("acceder", policy =>
                {
                    // Ajusta or�genes seg�n tu front-end; por defecto permite cualquier origen.
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            services.AddControllers().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.DefaultIgnoreCondition =
                    System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

            AddSwaggerConfiguration(services);

            AddAuthenticationConfiguration(services, configuration);
            // Add other infrastructure services here
            return services;
        }
        
        private static void AddAuthenticationConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            // Configura la autenticaci�n JWT aqu�
            // Ejemplo:
            /*
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });
            */

            // Configurar JWT Authentication
            var jwtKey = configuration["Jwt:Key"];
            var jwtIssuer = configuration["Jwt:Issuer"];
            var jwtAudience = configuration["Jwt:Audience"];

            if (string.IsNullOrWhiteSpace(jwtKey))
            {
                throw new InvalidOperationException("Jwt:Key no est� configurado en appsettings");
            }

            var key = Encoding.UTF8.GetBytes(jwtKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = !string.IsNullOrWhiteSpace(jwtIssuer),
                    ValidateAudience = !string.IsNullOrWhiteSpace(jwtAudience),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });


        }

        private static void AddSwaggerConfiguration(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddEndpointsApiExplorer();
            // Aquí puedes agregar la configuración de Swagger si es necesario
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Rifas API",
                    Version = "v1",
                    Description = "API para la gesti�n de rifas, tickets y transacciones."
                });
                // JWT in Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Authorization header usando el esquema Bearer. Ej: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                //c.AddSecurityRequirement(openSecurityReq);       
            });

            // (Puedes mantener AddOpenApi si necesitas, no es obligatorio)
            services.AddOpenApi();
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
