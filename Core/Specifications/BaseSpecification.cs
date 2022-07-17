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

      public Expression<Func<TEntity, object>> OrderBy { get; private set; }

      public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

      public int Take { get; private set; }

      public int Skip { get; private set; }

      public bool IsApplyPaging { get; private set; }

      protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
      {
         includes.Add(includeExpression);
      }

      protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
      {
         OrderBy = orderByExpression;
      }
      protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
      {
         OrderByDescending = orderByDescendingExpression;
      }

      protected void ApplyPaging(int skip, int take)
      {
         Take = take;
         Skip = skip;
         IsApplyPaging = true;
      }
   }
}