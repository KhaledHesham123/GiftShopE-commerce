using MediatR;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Interfaces;

namespace ProductCatalogService.Features.ProductFeatures.DeleteProduct
{
    public class DeleteProductCommandHandler(IRepository<Product> _ProductRepository , IUnitOfWork _unitOfWork) : IRequestHandler<DeleteProductCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product =await _ProductRepository.GetByIdAsync(request.productId , cancellationToken);
            if (product == null)
                 return Result<string>.FailResponse("Product not found", statusCode: 404);
            await _ProductRepository.DeleteAsync(product.Id , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<string>.SuccessResponse(product.Id.ToString(), "Product deleted successfully");
        }
    }
}
