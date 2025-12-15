using FluentValidation;

namespace CartService.Feature.ShoppingCartFeature.Commands.addtProducToCart
{
    public class addProducToCartCommandValidator: AbstractValidator<addProducToCartCommand>
    {
        public addProducToCartCommandValidator()
        {
            RuleFor(x => x.ShoppingCartId)
                .NotEmpty().WithMessage("The ShoppingCartId is required.");

            RuleFor(x => x.userid)
                .NotEmpty().WithMessage("The user ID is required."); 

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("The product ID is required."); 

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("The product name is required.")
                .MaximumLength(100).WithMessage("The product name must not exceed 100 characters.");

            RuleFor(x => x.ProductPrice)
                .GreaterThan(0).WithMessage("The product price must be greater than zero.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("The quantity must be greater than zero.")
                .LessThanOrEqualTo(1000).WithMessage("The quantity must not exceed 1000.");

            RuleFor(x => x.ProductImageUrl)
                .MaximumLength(500).WithMessage("The image URL is too long.");
        }

    }
    
    
}
