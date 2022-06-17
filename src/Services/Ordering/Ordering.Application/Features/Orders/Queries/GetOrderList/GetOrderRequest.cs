using MediatR;
using System.Collections.Generic;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderRequest : IRequest<List<GetOrderResponse>>
    {

        public GetOrderRequest(string userName)
        {
            UserName = userName;
        }
        public string UserName { get; set; }
    }
}