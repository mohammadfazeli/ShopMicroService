using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Features.Orders.Commands.AddOrder;
using Ordering.Application.Models;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckOutOrder
{
    public class CheckOutOrderCommandHandler : IRequestHandler<CheckOutOrderCommand, CheckOutOrderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepositpry;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckOutOrderCommandHandler> _logger;

        public CheckOutOrderCommandHandler(IMapper mapper,
                                      IOrderRepository orderRepositpry,
                                      IEmailService emailService,
                                      ILogger<CheckOutOrderCommandHandler> logger)
        {
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            _orderRepositpry = orderRepositpry ?? throw new System.ArgumentNullException(nameof(orderRepositpry));
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<CheckOutOrderResponse> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = _mapper.Map<Order>(request);

            Order orderModel = await _orderRepositpry.AddAsync(order);
            _logger.LogInformation($"order regsitered {orderModel.Id}");

            await SendEmail(orderModel);
            return _mapper.Map<CheckOutOrderResponse>(orderModel);
        }

        private async Task SendEmail(Order orderModel)
        {
            var email = new Email()
            {
                To = orderModel.EmailAddress,
                Body = $"order {orderModel.Id} regsitered ",
                Subject = "Register order"
            };
            try
            {
                await _emailService.Send(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"order failed " +
                    $" order id : {orderModel.Id}  " +
                    $" message :" +
                    $"{ex.Message} ");
            }
        }
    }
}