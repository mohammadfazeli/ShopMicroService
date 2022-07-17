using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Orders.Commands.CheckOutOrder;

namespace Basket.API.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMapper _maaper;
        private readonly IMediator _mediator;
        private readonly ILogger<BasketCheckoutConsumer> _logger;

        public BasketCheckoutConsumer(IMapper maaper, IMediator mediator, ILogger<BasketCheckoutConsumer> logger)
        {
            _maaper = maaper ?? throw new ArgumentNullException(nameof(maaper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var message = _maaper.Map<CheckOutOrderCommand>(context.Message);

            await _mediator.Send(message);
            _logger.LogInformation($" BasketCheckoutEvent  ok ");
        }
    }
}