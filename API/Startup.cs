using API.Errors;
using API.Helpers;
using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API
{
   public class Startup
   {
      private IConfiguration _config { get; }

      public Startup(IConfiguration config)
      {
         _config = config;
      }


      // * Dependency Injection 
      // * Register our services to services container of framework
      // * Framework will instantiate the instances of class when we provide services in the parameter of that class
      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         // Register our ProductRepository into IServiceCollection Container
         services.AddScoped<IProductRepository, ProductRepository>();
         services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
         services.AddAutoMapper(typeof(MappingProfiles));
         services.AddControllers();
         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
         });
         //  Register StoreContext into IServicesCollection Container
         services.AddDbContext<StoreContext>(x => x.UseSqlServer(_config.GetConnectionString("DefaultConnection")));
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
      }

      // * Middleware
      // * Allow us to access and perform some work in incoming request and outgoing response
      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         // Provide some information about exception when develop application
         // Internal Server Exception Handling
         app.UseMiddleware<ExceptionMiddleware>();

         app.UseSwagger();
         app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));

         // Not found end point error handling
         app.UseStatusCodePagesWithReExecute("/api/error/{0}");

         // When we access HTTP port - it will automatically redirect to HTTPs
         app.UseHttpsRedirection();

         // Decide what controller endpoint should be hitted
         app.UseRouting();
         app.UseStaticFiles();

         // Decide what resources user can be accessed, what permission of user
         app.UseAuthorization();

         // Execute selected controller endpoint 
         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
