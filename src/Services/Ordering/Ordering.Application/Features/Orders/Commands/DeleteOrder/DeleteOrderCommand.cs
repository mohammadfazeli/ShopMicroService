using MediatR;

namespace Ordering.Application.Features.Orders.Commands.AddOrder
{
    public class DeleteOrderCommand : IRequest
    {

        public int Id { get; set; }
    
    }
}