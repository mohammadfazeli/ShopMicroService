using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidation : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidation()
        {
            RuleFor(s => s.UserName)
                .NotNull()
                .NotEmpty().WithMessage("user  is emty")
                .MaximumLength(100).WithMessage("Maximum Length must be : 100");
        }
    }
}