using FluentValidation;

namespace ProductCatalogService.Features.ProductFeatures.ActivateProduct
{
    public class ActivateProductValidator : AbstractValidator<ActivateProductCommand>
    {
        public ActivateProductValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product id is required.");
        }
    }
}
