using FluentValidation;

namespace ProductCatalogService.Features.ProductFeatures.Commands.UpdateCartProductQuantity
{
    public class UpdateCartProductQuantityCommandValidator : AbstractValidator<UpdateCartProductQuantityCommand>
    {
        public UpdateCartProductQuantityCommandValidator()
        {
            RuleFor(x => x.userid)
                .NotEmpty()
                .WithMessage("BasketId is required.");

            RuleFor(x => x.Productid)
                .NotEmpty()
                .WithMessage("ProductId is required.");

            RuleFor(x => x.newQuantity)
                .LessThanOrEqualTo(100)
                .WithMessage("Quantity cannot exceed 100.");
        }
    }


}
