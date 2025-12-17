using Microsoft.Identity.Client;
using UserProfileService.Features.OccasionFeatures.Add.AddOccasion.Dto;
using UserProfileService.Features.Shared;

namespace UserProfileService.Shared.Interfaces
{
    public interface  IAddOccasionQr
    {
        Task<UserProfileService.Features.Shared.Result<bool>> IAddOccasion(OccasionRequest Request);
    }
}
