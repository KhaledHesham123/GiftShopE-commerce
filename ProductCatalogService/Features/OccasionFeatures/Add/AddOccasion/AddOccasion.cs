using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using UserProfileService.Features.OccasionFeatures.Add.AddOccasion.Dto;
using UserProfileService.Features.Shared;
using UserProfileService.Shared.Entities;
using UserProfileService.Shared.Interfaces;
using UserProfileService.Shared.Repositories;

namespace UserProfileService.Features.OccasionFeatures.Add.AddOccasion
{
    public record AddOccasion( string Name , bool Status ,
    string? ImageUrl ):IRequest< Result<OccasionDro>>;

    public class AddOccasionCommandHandler(IRepository<UserProfileService.Shared.Entities.Occasion> _repository) : IRequestHandler<AddOccasion, Result<OccasionDro>>
    {
       
        public async Task<Result<OccasionDro>> Handle(AddOccasion request, CancellationToken cancellationToken)
        {
                var occasion = new UserProfileService.Shared.Entities.Occasion
                {
                    Name = request.Name,
                    Status = request.Status,
                    ImageUrl = request.ImageUrl
                };
                await _repository.AddAsync(occasion, cancellationToken);
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
