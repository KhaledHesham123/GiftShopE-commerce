using FluentValidation;

namespace ProductCatalogService.Features.ProductFeatures.Commands.AddProducTOCart
{
    public class AddProductToCartCommandValidator : AbstractValidator<AddProductToCartCommand>
    {
        public AddProductToCartCommandValidator()
        {
            RuleFor(x => x.userid)
               .NotEmpty()
               .WithMessage("UserId is required");

            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("ProductId is required");

            

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero");
        }
    }
}
