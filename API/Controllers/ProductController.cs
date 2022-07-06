using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ProductsController : ControllerBase
   {
      private readonly IGenericRepository<Product> _productRepos;
      private readonly IGenericRepository<ProductType> _productTypeRepos;
      private readonly IGenericRepository<ProductBrand> _productBrandRepos;
      private readonly IMapper _mapper;
      public ProductsController(IGenericRepository<Product> productRepos, IGenericRepository<ProductType> productTypeRepos,
      IGenericRepository<ProductBrand> productBrandRepos, IMapper mapper)
      {
         _mapper = mapper;
         _productBrandRepos = productBrandRepos;
         _productTypeRepos = productTypeRepos;
         _productRepos = productRepos;
         _mapper = mapper;
      }

      [HttpGet]
      public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
      {
         var spec = new ProductsWithTypesAndBrandsSpecification();
         var products = await _productRepos.ListAsync(spec);
         return Ok(products.Select(product => _mapper.Map<ProductToReturnDto>(product)));
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<Product?>> GetProduct(int id)
      {
         var spec = new ProductsWithTypesAndBrandsSpecification(id);
         var product = await _productRepos.GetEntityWithSpec(spec);
         return Ok(_mapper.Map<ProductToReturnDto>(product));
      }

      [HttpGet("brands")]
      public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
      {
         return Ok(await _productBrandRepos.ListAllAsync());
      }

      [HttpGet("types")]
      public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
      {
         return Ok(await _productTypeRepos.ListAllAsync());
      }
   }
}