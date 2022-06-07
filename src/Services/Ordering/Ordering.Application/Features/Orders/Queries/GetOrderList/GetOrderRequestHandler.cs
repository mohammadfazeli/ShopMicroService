using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderRequestHandler : IRequestHandler<GetOrderRequest, List<GetOrderResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepositpry;

        public GetOrderRequestHandler(IMapper mapper, IOrderRepository orderRepositpry)
        {
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            _orderRepositpry = orderRepositpry ?? throw new System.ArgumentNullException(nameof(orderRepositpry));
        }

        public async Task<List<GetOrderResponse>> Handle(GetOrderRequest request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepositpry.GetOrdersByUserName(request.UserName);
            return _mapper.Map<List<GetOrderResponse>>(orders);
        }
    }
}