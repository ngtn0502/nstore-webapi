using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API
{
   public class Program
   {
      public static async Task Main(string[] args)
      {
         var host = CreateHostBuilder(args).Build();
         using (var scope = host.Services.CreateScope())
         {
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
               var dbContext = services.GetRequiredService<StoreContext>();
               await dbContext.Database.MigrateAsync();
               await StoreContextSeed.SeedsAsync(dbContext, loggerFactory);
            }
            catch (Exception ex)
            {
               var logger = loggerFactory.CreateLogger<Program>();
               logger.LogError(ex, "An error occurred during migration!");
            }
         }
         host.Run();
      }

      public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                 webBuilder.UseStartup<Startup>();
              });
   }
}
