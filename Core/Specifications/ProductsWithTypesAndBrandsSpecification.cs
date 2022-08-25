using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Params;

namespace Core.Specifications
{
   public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
   {
      public ProductsWithTypesAndBrandsSpecification(ProductParams productParams)
         : base(x =>
         (
            (!(productParams.BrandId != null) || (x.ProductBrandId == productParams.BrandId)) &&
            (!(productParams.TypeId != null) || (x.ProductTypeId == productParams.TypeId)) &&
            ((string.IsNullOrEmpty(productParams.Search)) || (x.Name.ToLower().Contains(productParams.Search)))
         ))
      {
         base.AddInclude(p => p.ProductBrand);
         base.AddInclude(p => p.ProductType);
         base.AddInclude(p => p.ProductSize);
         base.AddOrderBy(p => p.Name);

         if (!String.IsNullOrEmpty(productParams.Sort))
         {
            switch (productParams.Sort)
            {
               case "priceAsc":
                  base.AddOrderBy(p => p.Price);
                  break;

               case "priceDesc":
                  base.AddOrderByDescending(p => p.Price);
                  break;
               default:
                  base.AddOrderBy(p => p.Name);
                  break;
            }
         }
         base.ApplyPaging((productParams.PageSize * (productParams.PageNumber - 1)), productParams.PageSize);
      }

      public ProductsWithTypesAndBrandsSpecification(int id) : base(p => p.Id == id)
      {
         base.AddInclude(p => p.ProductBrand);
         base.AddInclude(p => p.ProductType);
         base.AddInclude(p => p.ProductSize);
      }
   }
}