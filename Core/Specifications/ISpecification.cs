using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
   public interface ISpecification<T>
    {
        //this is where criteria
         Expression<Func<T, bool>> Criteria {get; }
         //includes 
         List<Expression<Func<T, object>>> Includes {get; }
    }
}