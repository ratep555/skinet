namespace Core.Entities.OrderAggregate
{
    //this will serve as a snapshot of productitem that was ordered based on the fact 
    //that productname and productimage might change...but we dont want to change our orders
    public class ProductItemOrdered
    {
        //we are also adding parentheless constructor since entityframework needs one
        //otherwise we will get some copmlaints while performing migrations       
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemId, string productName, string pictureUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}