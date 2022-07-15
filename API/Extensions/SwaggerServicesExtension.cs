using Microsoft.OpenApi.Models;

namespace API.Extensions
{
   public static class SwaggerServicesExtension
   {
      public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
      {
         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
         });
         return services;
      }

      public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
      {
         app.UseSwagger();
         app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
         return app;
      }
   }
}