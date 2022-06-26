using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ProductsController : ControllerBase
   {
      private IProductRepository _repos;
      public ProductsController(IProductRepository productRepository)
      {
         _repos = productRepository;
      }

      [HttpGet]
      public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
      {
         var products = await _repos.GetProductsAsync();
         return Ok(products);
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<Product?>> GetProduct(int id)
      {
         return await _repos.GetProductByIdAsync(id);
      }

      [HttpGet("brands")]
      public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
      {
         return Ok(await _repos.GetProductBrandsAsync());
      }

      [HttpGet("types")]
      public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
      {
         return Ok(await _repos.GetProductTypesAsync());
      }
   }
}