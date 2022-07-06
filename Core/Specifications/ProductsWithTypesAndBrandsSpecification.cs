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
      public ProductsWithTypesAndBrandsSpecification() : base()
      {
         base.AddInclude(p => p.ProductBrand);
         base.AddInclude(p => p.ProductType);
      }

      public ProductsWithTypesAndBrandsSpecification(int id) : base(p => p.Id == id)
      {
         base.AddInclude(p => p.ProductBrand);
         base.AddInclude(p => p.ProductType);
      }
   }
}