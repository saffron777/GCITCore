using Microsoft.AspNetCore.Builder;
using Scalar.AspNetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GCIT.Core.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UseGCITCore(this WebApplication app)
        {

            app.MapOpenApi();
            app.MapScalarApiReference("/docs", options =>
            {
                // ---- Opciones de personalización ----
                options
                .WithTitle("Documentación de API")
                .WithOpenApiRoutePattern($"/swagger/v1/swagger.json")
                .WithTheme(ScalarTheme.DeepSpace);
                options.HideClientButton = true;
                options.WithDotNetFlag(true);
            });

            app.UseHttpsRedirection();

            // Usar CORS "acceder"
            app.UseCors("acceder");
            // Configurar autenticación y autorización
            // Autenticaci�n y autorizaci�n
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseSwagger();

            return app;
        }
    }
}
