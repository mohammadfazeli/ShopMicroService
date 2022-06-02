using Basket.API.Entites;
using Basket.API.GrpsServices;
using Basket.API.Repositories.Inerface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpsService _discountGrpsService;

        public BasketController(IBasketRepository basketRepository, DiscountGrpsService discountGrpsService)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _discountGrpsService = discountGrpsService ?? throw new ArgumentNullException(nameof(discountGrpsService));
        }

        [HttpGet("[action]/{userName}", Name = nameof(GetBasket))]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(ShoppingCard))]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ShoppingCard>> GetBasket(string userName)
        {
            ShoppingCard basket = await _basketRepository.GetBasket(userName);
            return basket == null ? NotFound() : Ok(basket);
        }

        [HttpPost("[action]", Name = nameof(AddOrUpdateBasket))]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(ShoppingCard))]
        public async Task<ActionResult<ShoppingCard>> AddOrUpdateBasket([FromBody] ShoppingCard shoppingCard)
        {
            foreach (var item in shoppingCard.ShoppingCardItems)
            {
                var coupon = await _discountGrpsService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            ShoppingCard basket = await _basketRepository.UpdateBasket(shoppingCard);
            return Ok(basket);
        }

        [HttpDelete("[action]/{userName}", Name = nameof(DeleteBasket))]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(ShoppingCard))]
        public async Task<ActionResult<ShoppingCard>> DeleteBasket(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }
    }
}