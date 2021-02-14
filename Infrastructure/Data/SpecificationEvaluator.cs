using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    //može ovdje biti samo T, ovo je da se bolje skuži
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, 
        ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);  //replaced with for example p => p.ProductTypeId == id
            }
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);  
            }
            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);  
            }
            //paging mora nakon orderby, u protivnom bismo orderby samo one that we took
             if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            //aggregate because we are combining all of our include operations
            //this method takes our (two) include statements, aggregates them and passes them in a query
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}