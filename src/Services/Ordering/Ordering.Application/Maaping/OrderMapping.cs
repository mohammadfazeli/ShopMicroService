using Ordering.Application.Features.Orders.Commands.CheckOutOrder;
using Ordering.Application.Features.Orders.Queries.GetOrderList;
using Ordering.Domain.Entities;

namespace Ordering.Application.Maaping
{
    public class OrderMapping : MappingProfile
    {
        public OrderMapping()
        {
            CreateMap<Order, GetOrderResponse>().ReverseMap();
            CreateMap<Order, GetOrderRequest>().ReverseMap();

            CreateMap<Order, CheckOutOrderResponse>().ReverseMap();
            CreateMap<Order, CheckOutOrderCommand>().ReverseMap();
        }
    }
}