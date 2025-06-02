using BasketService.API.Models;

namespace BasketService.API.Repositories;

public interface IBasketRepository
{
    Task<CustomerBasket?> GetBasket(string userId);
    Task<CustomerBasket> UpdateBasket(CustomerBasket basket);
    Task<bool> DeleteBasket(string userId);
}
