using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
   public static class ApplicationServicesExtension
   {
      public static IServiceCollection AddApplicationServices(this IServiceCollection services)
      {
         // Register our ProductRepository into IServiceCollection Container
         services.AddScoped<IProductRepository, ProductRepository>();
         services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
         // Configure modal state about api validation error response
         services.Configure<ApiBehaviorOptions>(options =>
         {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
               var errors = actionContext.ModelState
                              .Where(x => x.Value.Errors.Count() > 0)
                              .SelectMany(x => x.Value.Errors)
                              .Select(x => x.ErrorMessage).ToList();
               var response = new ApiValidationErrorResponse(errors);
               return new BadRequestObjectResult(response);
            };
         });
         return services;
      }
   }
}