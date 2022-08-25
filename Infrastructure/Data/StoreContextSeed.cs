using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
   public class StoreContextSeed
   {
      public static async Task SeedsAsync(StoreContext context, ILoggerFactory loggerFactory)
      {
         try
         {
            if (!context.ProductSizes.Any())
            {
               var productSizes = await File.ReadAllTextAsync("../Infrastructure/Data/SeedsData/sizes.json");
               var sizes = JsonSerializer.Deserialize<List<ProductSize>>(productSizes);
               await context.ProductSizes.AddRangeAsync(sizes);
               await context.SaveChangesAsync();
            }
            if (!context.ProductBrands.Any())
            {
               var productBrands = await File.ReadAllTextAsync("../Infrastructure/Data/SeedsData/brands.json");
               var brands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrands);
               await context.ProductBrands.AddRangeAsync(brands);
               await context.SaveChangesAsync();
            }
            if (!context.ProductTypes.Any())
            {
               var productTypes = await File.ReadAllTextAsync("../Infrastructure/Data/SeedsData/types.json");
               var types = JsonSerializer.Deserialize<List<ProductType>>(productTypes);
               await context.ProductTypes.AddRangeAsync(types);
               await context.SaveChangesAsync();
            }
            if (!context.Products.Any())
            {
               var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedsData/products.json");
               var products = JsonSerializer.Deserialize<List<Product>>(productsData);
               await context.Products.AddRangeAsync(products);
               await context.SaveChangesAsync();
            }
         }
         catch (Exception ex)
         {
            var logger = loggerFactory.CreateLogger<StoreContext>();
            logger.LogError(ex.Message, "Error occurred during seeding data!");
         }
      }
   }
}