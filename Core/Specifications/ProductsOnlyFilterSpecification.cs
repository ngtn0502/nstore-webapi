using Core.Entities;
using Core.Params;

namespace Core.Specifications
{
   public class ProductsOnlyFilterSpecification : BaseSpecification<Product>
   {
      public ProductsOnlyFilterSpecification(ProductParams productParams)
        : base(x =>
        (
            (!(productParams.BrandId != null) || (x.ProductBrandId == productParams.BrandId)) &&
            (!(productParams.TypeId != null) || (x.ProductTypeId == productParams.TypeId)) &&
            ((string.IsNullOrEmpty(productParams.Search)) || (x.Name.Contains(productParams.Search)))
        ))
      {
      }
   }
}