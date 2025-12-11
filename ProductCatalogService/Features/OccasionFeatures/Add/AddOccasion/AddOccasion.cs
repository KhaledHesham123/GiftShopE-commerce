using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductCatalogService.Features.OccasionFeatures.Add.AddOccasion.Dto;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Interfaces;
using ProductCatalogService.Shared.Repositories;

namespace ProductCatalogService.Features.OccasionFeatures.Add.AddOccasion
{
    public record AddOccasion( string Name , bool Status ,
    string? ImageUrl ):IRequest< Result<OccasionDro>>;

    public class AddOccasionCommandHandler(IRepository<ProductCatalogService.Shared.Entities.Occasion> _repository) : IRequestHandler<AddOccasion, Result<OccasionDro>>
    {
       
        public async Task<Result<OccasionDro>> Handle(AddOccasion request, CancellationToken cancellationToken)
        {
                var occasion = new ProductCatalogService.Shared.Entities.Occasion
                {
                    Name = request.Name,
                    Status = request.Status,
                    ImageUrl = request.ImageUrl
                };
                await _repository.AddAsync(occasion);
                var occasiondro = new OccasionDro
                {
                    Id= occasion.Id,
                    Name = occasion.Name,
                    Status = occasion.Status,
                    ImageUrl = occasion.ImageUrl
                };
            return Result<OccasionDro>.SuccessResponse(occasiondro, "Occasion added successfully", 201);
        }
    }
}
