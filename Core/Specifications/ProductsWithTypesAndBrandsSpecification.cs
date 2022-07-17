using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
   public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
   {
      public ProductsWithTypesAndBrandsSpecification(string sort, int? brandId = null, int? typeId = null, int pageSize = 6, int pageNumber = 1)
         : base(x =>
         (
            (!(brandId != null) || (x.ProductBrandId == brandId)) &&
            (!(typeId != null) || (x.ProductTypeId == typeId))
         ))
      {
         base.AddInclude(p => p.ProductBrand);
         base.AddInclude(p => p.ProductType);
         base.AddOrderBy(p => p.Name);

         if (!String.IsNullOrEmpty(sort))
         {
            switch (sort)
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
         pageSize = pageSize > 50 ? 6 : pageSize;
         base.ApplyPaging((pageSize * (pageNumber - 1)), pageSize);
      }

      public ProductsWithTypesAndBrandsSpecification(int id) : base(p => p.Id == id)
      {
         base.AddInclude(p => p.ProductBrand);
         base.AddInclude(p => p.ProductType);
      }
   }
}