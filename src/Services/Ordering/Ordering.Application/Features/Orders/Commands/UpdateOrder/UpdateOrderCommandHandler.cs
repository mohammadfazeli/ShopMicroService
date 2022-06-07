using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepositpry;
        private readonly IEmailService _emailService;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IMapper mapper,
                                      IOrderRepository orderRepositpry,
                                      IEmailService emailService,
                                      ILogger<UpdateOrderCommandHandler> logger)
        {
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            _orderRepositpry = orderRepositpry ?? throw new System.ArgumentNullException(nameof(orderRepositpry));
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _orderRepositpry.GetByIdAsync(request.Id);

            if (order == null)
            {
                _logger.LogError($"Order {request.Id} Not Founded");
                throw new NotFoundException(nameof(order), request.Id);

            }

            Order editOrder = _mapper.Map<Order>(request);

            return await _orderRepositpry.UpdateAsync(editOrder);

        }
    }
}