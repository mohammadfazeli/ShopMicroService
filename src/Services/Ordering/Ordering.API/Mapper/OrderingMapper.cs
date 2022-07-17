using EventBus.Messages.Events;
using Ordering.API.Mapper;
using Ordering.Application.Features.Orders.Commands.CheckOutOrder;

namespace Basket.API.Mapper
{
    public class OrderingMapper : CustomMapperConfig
    {
        public OrderingMapper()
        {
            CreateMap<CheckOutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}