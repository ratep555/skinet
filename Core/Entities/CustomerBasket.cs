using System.Collections.Generic;

namespace Core.Entities
{
    public class CustomerBasket
    {
        //stavili smo i prazni kako bi redis mogao instancirati novi basket bez potrebe da zna id
        public CustomerBasket()
        {
        }
        //we dont need list here because we are initialising it down there
        //automatski se izgenerira id prilikom instanciranja
        public CustomerBasket(string id)
        {
            Id = id;
        }
        //our angular application is gonna generate the id
        public string Id { get; set; }
        //we are initialising new list of items
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}