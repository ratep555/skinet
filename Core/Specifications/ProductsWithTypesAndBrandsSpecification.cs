using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            //možemo koristiti addinclude zbog basespecification
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        //sada koristimo drugi konstruktor sa parametrima, obzirom da
        //u basespecification imamo dva konstruktora, ovaj u sebi ima criteria
        //x => x.Id == id zamjenjuje Expression<Func<T, bool>> criteria, to ti je filter iz uplift!
        //also while you are there, include two other properties
        public ProductsWithTypesAndBrandsSpecification(int id) 
            : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}