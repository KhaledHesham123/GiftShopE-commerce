using FluentValidation;

namespace OrderService.Feature.OrderFeature.Commands.selectPaymentMethod
{
    public class selectPaymentMethodCommandValidator:AbstractValidator<selectPaymentMethodCommand>
    {
        public selectPaymentMethodCommandValidator()
        {
            RuleFor(x => x.orderid)
               .NotEmpty().WithMessage("Order Id must be provided.");

            RuleFor(x => x.PaymentMethods)
                .IsInEnum().WithMessage("Invalid payment method.");
        }
    }
}
