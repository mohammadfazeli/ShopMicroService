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
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
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

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepositpry.GetByIdAsync(request.Id);

            if (orderToUpdate == null)
            {
                _logger.LogError($"Order {request.Id} Not Founded");
                throw new NotFoundException(nameof(orderToUpdate), request.Id);
            }

            //Order editOrder = _mapper.Map<Order>(request);
            _mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));

            await _orderRepositpry.UpdateAsync(orderToUpdate);
            return Unit.Value;
        }
    }
}