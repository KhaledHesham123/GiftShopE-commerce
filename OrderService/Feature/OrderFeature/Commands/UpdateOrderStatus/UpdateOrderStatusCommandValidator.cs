using FluentValidation;
using OrderService.Shared.Entites;

namespace OrderService.Feature.OrderFeature.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(x => x.orderid)
                .NotEmpty().WithMessage("OrderId is required.");

            RuleFor(x => x.orderStatus)
                .IsInEnum().WithMessage("Invalid Order Status.");

            When(x => x.orderStatus == OrderStatus.OutForDelivery, () => {
                RuleFor(x => x.DeliveryHeroName)
                    .NotEmpty().WithMessage("Delivery Hero Name is required when status is OutForDelivery.");

                RuleFor(x => x.DeliveryHeroContact)
                    .NotEmpty().WithMessage("Delivery Hero Contact is required when status is OutForDelivery.");

                RuleFor(x => x.EstimatedArrivalTime)
                    .NotEmpty().WithMessage("Estimated Arrival Time is required when order is out for delivery.");
            });


        }
    }
    
    
}
