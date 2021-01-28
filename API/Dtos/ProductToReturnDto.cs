namespace API.Dtos
{
    //dto is a container for moving objects between layers
    //ušminkavamo objekt koji želimo vratiti klijentu
     public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }
    }
}