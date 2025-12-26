using FluentValidation;

namespace ProductCatalogService.Features.ProductFeatures.Queries.GetProductsByids
{
    public class GetProductsByidsQueryValidator: AbstractValidator<GetProductsByidsQuery>
    {
        public GetProductsByidsQueryValidator()
        {
            RuleFor(x => x.ProductsIds)
            .NotNull().WithMessage("ProductIds cannot be null")
            .Must(ids => ids.Any()).WithMessage("ProductIds cannot be empty");
        }
    }
}
