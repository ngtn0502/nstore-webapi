using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
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
      }

      // * Middleware
      // * Allow us to access and perform some work in incoming request and outgoing response
      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         // Provide some information about exception when develop application
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
         }

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
