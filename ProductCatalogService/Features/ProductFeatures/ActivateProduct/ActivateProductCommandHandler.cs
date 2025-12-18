using MediatR;
using ProductCatalogService.Features.CategoryFeatures.Commands.ActivateCategory;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Interfaces;

namespace ProductCatalogService.Features.ProductFeatures.ActivateProduct
{
    public class ActivateProductCommandHandler(IRepository<Product> _productRepository) : IRequestHandler<ActivateProductCommand, Result<ActivateProductDto>>
    {
        public async Task<Result<ActivateProductDto>> Handle(ActivateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product == null)
                return Result<ActivateProductDto>.FailResponse("Product not found.", new List<string> { "Product not found." }, 404);

            product.IsActive = request.IsActive;

            _productRepository.SaveInclude(product, nameof(product.IsActive));

            return Result<ActivateProductDto>.SuccessResponse(new ActivateProductDto { Id = product.Id, IsActive = product.IsActive }, "Product status updated successfully.", 200);
        }
    }
}
