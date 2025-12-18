using MediatR;
using ProductCatalogService.Features.OccasionFeatures.Add.AddOccasion;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Interfaces;

namespace ProductCatalogService.Features.ProductFeatures.CreateProduct
{
    public class CreateProductCommandHandler(IRepository<Category> _categoryRepository , IRepository<Occasion> _occasionRepository,IRepository<Product> _productRepository,IRepository<Tag> _tagRepository ,IImageHelper _imageHelper,IUnitOfWork _unitOfWork) : IRequestHandler<CreateProductCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var categoryIsExist =await _categoryRepository.ExistsAsync(request.CategoryId , cancellationToken);
            if (!categoryIsExist)
                return Result<Guid>.FailResponse($"Category with Id = {request.CategoryId} not found");
            if (request.Images == null || !request.Images.Any())
                return Result<Guid>.FailResponse("At least one image is required");
            if (request.OccasionIds == null || !request.OccasionIds.Any())
                return Result<Guid>.FailResponse("Product must have at least one occasion");
            bool occasionIsExist;
            foreach (var Id in request.OccasionIds)
            {
                occasionIsExist =await _occasionRepository.ExistsAsync(Id, cancellationToken);
                if (!occasionIsExist)
                    return Result<Guid>.FailResponse($"Occasion with Id = {Id} not found");
            }
            var product = new Product
            {
                 Name = request.Name,
                  Description = request.Description,
                  Price = request.Price,
                  IsActive = request.IsActive,
                  CategoryId = request.CategoryId,
            };
            foreach ( var id in request.OccasionIds.Distinct())
            {
                product.ProductOccasions.Add(new ProductOccasion
                {
                    OccasionId = id,                 
                });
            }
            foreach(var tagName in request.Tags)
            {
                var tag =await _tagRepository.FirstOrDefaultAsync(t => t.Name.ToLower() == tagName.ToLower() , cancellationToken);
                if (tag == null)
                {
                    tag = new Tag
                    {
                        Name = tagName
                    };
                }             
                product.Tags .Add(new ProductTag
                {
                   Tag = tag
                });
            }

            foreach (var image in request.Images)
            {
                var imageUrl = await _imageHelper.SaveImageAsync(image ,"Products");
                product.Images.Add( new ProductImage
                {
                    ImageUrl = imageUrl
                });
            }
            foreach (var attribute in request.Attributes)
            {
                var productAttribute = new ProductAttribute
                {
                    Name = attribute.Name,
                    Quentity = attribute.Quentity,
                };
                product.Attributes .Add(productAttribute);
            }
            await _productRepository.AddAsync(product , cancellationToken);
            await _unitOfWork.SaveChangesAsync();
            return Result<Guid>.SuccessResponse(product.Id , "Product created successfully");
        }
    }
}
