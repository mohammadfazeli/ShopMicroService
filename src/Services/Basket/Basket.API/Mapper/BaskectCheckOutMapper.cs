using Basket.API.Entites;
using EventBus.Messages.Events;

namespace Basket.API.Mapper
{

    public class BaskectCheckOutMapper : CustomMapperConfig
    {
        public BaskectCheckOutMapper()
        {
            CreateMap<basketCheckOut, BasketCheckoutEvent>().ReverseMap();
        }
    }
}