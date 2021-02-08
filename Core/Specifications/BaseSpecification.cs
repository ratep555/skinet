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

        //private set because we are adding the ability to set what this is inside
        //this particular class
        public Expression<Func<T, object>> OrderBy {get; private set;}

        public Expression<Func<T, object>> OrderByDescending {get; private set;}

        public int Take {get; private set;}

        public int Skip {get; private set;}

        public bool IsPagingEnabled {get; private set;}

        

        //these two methods down need to be evaluated by specificationevaluator
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

    }
}