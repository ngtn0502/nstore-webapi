using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Specifications
{
   public static class SpecificationEvaluator<T> where T : BaseEntity
   {
      public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> specification)
      {
         var currentQuery = query;

         if (specification.Criteria != null)
         {
            currentQuery = currentQuery.Where(specification.Criteria);
         }

         if (specification.OrderBy != null)
         {
            currentQuery = currentQuery.OrderBy(specification.OrderBy);
         }

         if (specification.OrderByDescending != null)
         {
            currentQuery = currentQuery.OrderByDescending(specification.OrderByDescending);
         }

         currentQuery = specification.Includes.Aggregate(currentQuery, (current, include) => current.Include(include));

         return currentQuery;
      }
   }
}