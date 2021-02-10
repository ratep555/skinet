using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    //stvaramo novi interface, generic interface je za eframework, ovaj je za redis
   public interface IBasketRepository
    {
         Task<CustomerBasket> GetBasketAsync(string basketId);
         Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
         Task<bool> DeleteBasketAsync(string basketId);
    }
}