using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        //where clause
        public Expression<Func<T, bool>> Criteria {get; }

        //this will have a list of includes statements that we can pass to our tolistasync method
        //we are setting this by default to empty list
        public List<Expression<Func<T, object>>> Includes {get; } = 
            new List<Expression<Func<T, object>>>();

        //this method allows us to add include statements to our include list
        //protected means that we can access this method from this class and any class that 
        //derives from this class (child classes)
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}