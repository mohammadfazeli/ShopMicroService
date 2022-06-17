using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersRequestHandler : IRequestHandler<GetAllOrdersRequest, List<GetAllOrdersResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepositpry;

        public GetAllOrdersRequestHandler(IMapper mapper, IOrderRepository orderRepositpry)
        {
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            _orderRepositpry = orderRepositpry ?? throw new System.ArgumentNullException(nameof(orderRepositpry));
        }

        public async Task<List<GetAllOrdersResponse>> Handle(GetAllOrdersRequest request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Domain.Entities.Order> orders = await _orderRepositpry.GetAllAsync();
            List<GetAllOrdersResponse> x = _mapper.Map<List<GetAllOrdersResponse>>(orders);
            return x;
        }
    }
}