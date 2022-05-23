using Basket.API.Entites;
using Basket.API.Repositories.Inerface;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCash;

        public BasketRepository(IDistributedCache distributedCache)
        {
            _redisCash = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCash.RemoveAsync(userName);
        }

        public async Task<ShoppingCard> GetBasket(string userName)
        {
            var item = await _redisCash.GetStringAsync(userName);
            if (string.IsNullOrEmpty(item))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCard>(item);
        }

        public async Task<ShoppingCard> UpdateBasket(ShoppingCard shoppingCard)
        {
            await _redisCash.SetStringAsync(shoppingCard.UserName, JsonConvert.SerializeObject(shoppingCard));
            return await GetBasket(shoppingCard.UserName);
        }
    }
}