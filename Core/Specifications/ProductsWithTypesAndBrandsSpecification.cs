using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
       //šalješ ovo kao opcijske parametre
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
         : base(x =>
               (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
               (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
               (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
               )
              
        {
            //možemo koristiti addinclude zbog basespecification
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            //default orderby name, daje proizvode alfabetski povezane
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

           if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
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