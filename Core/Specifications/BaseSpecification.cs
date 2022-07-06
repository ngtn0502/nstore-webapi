using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
   public class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : BaseEntity
   {
      public BaseSpecification(Expression<Func<TEntity, bool>> cri)
      {
         criteria = cri;
      }
      public BaseSpecification()
      {
      }
      private Expression<Func<TEntity, bool>> criteria;
      private List<Expression<Func<TEntity, object>>> includes = new List<Expression<Func<TEntity, object>>>();
      public Expression<Func<TEntity, bool>> Criteria
      {
         get { return criteria; }
      }
      public List<Expression<Func<TEntity, object>>> Includes
      {
         get { return includes; }
      }
      protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
      {
         includes.Add(includeExpression);
      }
   }
}