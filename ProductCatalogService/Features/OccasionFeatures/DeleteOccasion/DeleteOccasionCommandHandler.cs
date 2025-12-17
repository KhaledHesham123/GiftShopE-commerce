using MediatR;
using Microsoft.EntityFrameworkCore;
using UserProfileService.Features.Shared;
using UserProfileService.Shared.Entities;
using UserProfileService.Shared.Interfaces;

namespace UserProfileService.Features.OccasionFeatures.DeleteOccasion
{
    public class DeleteOccasionCommandHandler(IRepository<Occasion> _occasionRepository , IUnitOfWork _unitOfWork) : IRequestHandler<DeleteOccasionCommand, Result<DeleteOccasionDTO>>
    {
        public async Task<Result<DeleteOccasionDTO>> Handle(DeleteOccasionCommand request, CancellationToken cancellationToken)
        {
            var occasion =await _occasionRepository.GetAll()
                                                   .Include(o => o.ProductOccasions)
                                                   .FirstOrDefaultAsync(o => o.Id == request.occasionId, cancellationToken);
            if (occasion == null)
                return Result<DeleteOccasionDTO>.FailResponse("Occasion not found", statusCode: 404);
          
            if (occasion.ProductOccasions.Any())
                return Result<DeleteOccasionDTO>.FailResponse("Cannot delete occasion with associated products", statusCode: 400);

            occasion.Status = false;
            await _occasionRepository.DeleteAsync(occasion.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
            return Result<DeleteOccasionDTO>.SuccessResponse(new DeleteOccasionDTO { Id = occasion.Id}, "Occasion deleted successfully");
        }
    }
}
