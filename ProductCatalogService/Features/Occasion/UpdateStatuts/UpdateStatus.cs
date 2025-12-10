using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using ProductCatalogService.Features.Occasion.Add.AddOccasion.Dto;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Shared.Hup;
using ProductCatalogService.Shared.Interfaces;

namespace ProductCatalogService.Features.Occasion.UpdateStatuts
{
    public record UpdateStatus(Guid id , bool Status):IRequest<Result<bool>>;

    public class UpdateStatusHandler(IRepository<ProductCatalogService.Shared.Entities.Occasion> _repository, IHubContext<OccasionHub> _hub , IImageHelper image) : IRequestHandler<UpdateStatus, Result<bool>>
    {

        public async Task<Result<bool>> Handle(UpdateStatus request, CancellationToken cancellationToken)
        {
            var occasion = await _repository.GetByIdAsync(request.id, cancellationToken);
            if (occasion == null)
                return Result<bool>.FailResponse("Occasion not found", new List<string> {"Occasion not found" }, 404);
            occasion.Status = request.Status;
            _repository.SaveInclude(
                occasion,
                nameof(occasion.Status));
            //hub
            await _hub.Clients.All.SendAsync("ReceiveOccasionUpdateStatus", new OccasionDro
            {
                Id = request.id,
                Name = occasion.Name,
                Status = occasion.Status,
                ImageUrl = image.GetImageUrl(occasion.ImageUrl)
            });
            return Result<bool>.SuccessResponse(true, "Occasion status updated successfully", 200);
        }
    }
}
