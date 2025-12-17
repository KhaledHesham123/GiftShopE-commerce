using FluentValidation;

namespace ProductCatalogService.Features.ProductFeatures.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.OccasionIds).NotEmpty();
            RuleFor(x => x.Images).NotEmpty();
        }
    }
}
