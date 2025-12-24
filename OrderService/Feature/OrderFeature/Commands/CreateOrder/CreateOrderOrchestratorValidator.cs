using FluentValidation;

namespace OrderService.Feature.OrderFeature.Commands.CreateOrder
{
    public class CreateOrderOrchestratorValidator:AbstractValidator<CreateOrderOrchestrator>
    {
        public CreateOrderOrchestratorValidator()
        {
            RuleFor(x => x.UserId)
           .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.ShippingAddress)
                .NotEmpty().WithMessage("Shipping address is required.")
                .MaximumLength(300);

            RuleFor(x => x.RecipientName)
                .NotEmpty().WithMessage("Recipient name is required.")
                .MaximumLength(100);

            RuleFor(x => x.RecipientPhone)
                .NotEmpty().WithMessage("Recipient phone is required.")
                .Matches(@"^\+?[0-9]{10,15}$")
                .WithMessage("Invalid phone number format.");

            RuleFor(x => x.PaymentMethod)
                .IsInEnum()
                .WithMessage("Invalid payment method.");

            RuleFor(x => x.PointsRedeemed)
                .GreaterThanOrEqualTo(0)
                .WithMessage("PointsRedeemed cannot be negative.");

            RuleFor(x => x.CurrentLat)
                .NotNull()
                .When(x => x.CurrentLng.HasValue)
                .WithMessage("Latitude is required when Longitude is provided.");

            RuleFor(x => x.CurrentLng)
                .NotNull()
                .When(x => x.CurrentLat.HasValue)
                .WithMessage("Longitude is required when Latitude is provided.");
        }
    }
}
