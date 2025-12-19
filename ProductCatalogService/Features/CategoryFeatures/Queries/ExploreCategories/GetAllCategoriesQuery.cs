using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Interfaces;

namespace ProductCatalogService.Features.CategoryFeatures.Queries.ExploreCategories
{
    public record GetAllCategoriesQuery:IRequest<Result<IEnumerable<CategoryToReturnDto>>>;

    public class GetAllCategoriesQueryHandler:IRequestHandler<GetAllCategoriesQuery, Result<IEnumerable<CategoryToReturnDto>>>
    {
        private readonly IRepository<Category> repository;

        public GetAllCategoriesQueryHandler(IRepository<Category> repository)
        {
            this.repository = repository;
        }
        public async Task<Result<IEnumerable<CategoryToReturnDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories= await repository.GetAll().Where(x => x.Status==false).Select(x=>new CategoryToReturnDto
            {
                Name = x.Name,
                ImageUrl = x.ImageUrl,
            }).ToListAsync(cancellationToken);

            if (categories==null)
            {
                return Result<IEnumerable<CategoryToReturnDto>>.FailResponse("there is no categories");
            }
            return Result<IEnumerable<CategoryToReturnDto>>.SuccessResponse(categories);
        }
    }



}
