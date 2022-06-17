using MediatR;
using System.Collections.Generic;

namespace Ordering.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersRequest : IRequest<List<GetAllOrdersResponse>>
    {
        public GetAllOrdersRequest()
        {
        }
    }
}