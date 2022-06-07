using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckOutOrder
{
    public class CheckOutOrderCommandValidation : AbstractValidator<CheckOutOrderCommand>
    {

        public CheckOutOrderCommandValidation()
        {
            RuleFor(s => s.UserName)
                .NotNull()
                .NotEmpty().WithMessage("user  is emty")
                .MaximumLength(100).WithMessage("Maximum Length must be : 100");

        }
    }
}