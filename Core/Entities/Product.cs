namespace Core.Entities
{
    //we are deriving classes from baseentity
    //baseentity holds id
    public class Product : BaseEntity
    {
        // required i ostale stvari si stavio u Infrastructure.Data.Config.ProductConfiguration
        // lekcija 26
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
    }
}