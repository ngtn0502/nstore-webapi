using API.Extensions;
using API.Helpers;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
         services.AddAutoMapper(typeof(MappingProfiles));
         services.AddControllers();
         // Extensions add/register swagger services
         services.AddSwaggerServices();
         // Register StoreContext into IServicesCollection Container
         services.AddDbContext<StoreContext>(x => x.UseSqlServer(_config.GetConnectionString("DefaultConnection")));
         // Extensions add/register Repository and Config ApiBehaviorOptions
         services.AddApplicationServices();
      }

      // * Middleware
      // * Allow us to access and perform some work in incoming request and outgoing response
      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         // Provide some information about exception when develop application
         // Internal Server Exception Handling
         app.UseMiddleware<ExceptionMiddleware>();

         // Extensions add swagger to middleware pipeline 
         app.UseSwaggerDocumentation();

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
