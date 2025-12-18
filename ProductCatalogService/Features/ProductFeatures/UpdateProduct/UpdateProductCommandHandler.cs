using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Helper;
using ProductCatalogService.Shared.Interfaces;

namespace ProductCatalogService.Features.ProductFeatures.UpdateProduct
{
    public class UpdateProductCommandHandler(IRepository<Product> _productRepository , IRepository<Category> _categoryRepository , IRepository<Occasion> _occasionRepository , IRepository<Tag> _tagRepository , IImageHelper _imageHelper , IUnitOfWork _unitOfWork , IRepository<ProductImage> _imageRepository)  : IRequestHandler<UpdateProductCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product =await _productRepository.GetAll()
                                            .Include(p => p.ProductOccasions)
                                            .Include(p => p.Tags)
                                            .Include(p => p.Images)
                                            .FirstOrDefaultAsync(p => p.Id == request.ProductId);
            if (product == null)
                return Result<string>.FailResponse("Product not found");

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price= request.Price;
            product.IsActive = request.IsActive;
            //Categoery
            var categoryExists =await _categoryRepository.ExistsAsync(request.CategoryId, cancellationToken);
            if(!categoryExists)
                return Result<string>.FailResponse("Category not found", statusCode: 400);
            product.CategoryId = request.CategoryId;

            //Occasion
            product.ProductOccasions.Clear();

            foreach (var  Id in request.OccasionIds )
            {
                var OccasionExist = await _occasionRepository.ExistsAsync(Id, cancellationToken);
                if(!OccasionExist)
                    return Result<string>.FailResponse("Occasion not found", statusCode: 400);
                product.ProductOccasions.Add(new ProductOccasion
                {
                    ProductId = product.Id,
                    OccasionId = Id 
                });
            }
            //tags
            product.Tags.Clear();    
            foreach (var tagName in request.Tags)
            {
                var tag =await _tagRepository.FirstOrDefaultAsync(t => t.Name.ToLower() == tagName.ToLower(),cancellationToken);
                if(tag == null)
                {
                    tag = new Tag
                    {
                        Name = tagName,
                    };
                }
                product.Tags.Add(new ProductTag
                {
                    Tag = tag,
                });
            }

            //Images
            foreach (var image in product.Images.ToList())
            {
                _imageHelper.DeleteImage(image.ImageUrl, "Products");
                await _imageRepository.DeleteAsync(image.Id , cancellationToken);
            }
            product.Images.Clear();
            foreach (var file in request.NewImages)
            {
                var imageUrl = await _imageHelper.SaveImageAsync(file, "Products");
                product.Images.Add(new ProductImage
                {
                    ImageUrl = imageUrl
                });
            }
            //Attributes
            foreach (var attribute in request.Attributes)
            {
                product.Attributes.Add(new ProductAttribute
                {
                    Name = attribute.Name,
                    Quentity = attribute.Quentity
                });
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<string>.SuccessResponse("Product updated successfully");
        }
    }
}
