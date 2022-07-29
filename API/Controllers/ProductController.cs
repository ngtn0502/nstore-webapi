using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Params;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   public class ProductsController : BaseApiController
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
      public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductParams productParams)
      {
         var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
         var specWithoutPagination = new ProductsOnlyFilterSpecification(productParams);
         var products = await _productRepos.ListAsync(spec);
         var productsCount = await _productRepos.CountAsync(specWithoutPagination);
         var productsPagination = new Pagination<ProductToReturnDto>
         {
            PageNumber = productParams.PageNumber,
            PageSize = productsCount < productParams.PageSize ? productsCount : productParams.PageSize,
            Count = productsCount,
            Data = products.Select(product => _mapper.Map<ProductToReturnDto>(product))
         };
         return Ok(productsPagination);
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<Product?>> GetProduct(int id)
      {
         var spec = new ProductsWithTypesAndBrandsSpecification(id);
         var product = await _productRepos.GetEntityWithSpec(spec);
         if (product == null)
         {
            return NotFound(new ApiResponse(404));
         }
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