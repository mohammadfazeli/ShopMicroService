using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepositpry;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(
                                      IOrderRepository orderRepositpry,
                                      ILogger<DeleteOrderCommandHandler> logger)
        {
            _orderRepositpry = orderRepositpry ?? throw new System.ArgumentNullException(nameof(orderRepositpry));
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepositpry.GetByIdAsync(request.Id);

            if (order == null)
            {
                _logger.LogError($"Order {request.Id} Not Founded");
                throw new NotFoundException(nameof(order), request.Id);
            }

            await _orderRepositpry.DeleteAsync(order);
            return Unit.Value;
        }
    }
}