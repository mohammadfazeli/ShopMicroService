using Basket.API.Entites;

namespace Basket.API.Repositories.Inerface
{
    public interface IBasketRepository
    {
        Task<ShoppingCard> GetBasket(string userName);

        Task DeleteBasket(string userName);

        Task<ShoppingCard> UpdateBasket(ShoppingCard shoppingCard);
    }
}