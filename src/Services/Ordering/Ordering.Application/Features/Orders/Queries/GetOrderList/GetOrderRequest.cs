using MediatR;
using System.Collections.Generic;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderRequest : IRequest<List<GetOrderResponse>>
    {
        public string UserName { get; set; }
    }
}