using Microsoft.Identity.Client;
using ProductCatalogService.Features.OccasionFeatures.Add.AddOccasion.Dto;
using ProductCatalogService.Features.Shared;

namespace ProductCatalogService.Shared.Interfaces
{
    public interface  IAddOccasionQr
    {
        Task<ProductCatalogService.Features.Shared.Result<bool>> IAddOccasion(OccasionRequest Request);
    }
}
