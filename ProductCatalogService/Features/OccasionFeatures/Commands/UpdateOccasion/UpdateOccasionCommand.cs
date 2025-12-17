using MediatR;
using Microsoft.AspNetCore.SignalR;
using UserProfileService.Features.CategoryFeatures.Commands.UpdateCategory;
using UserProfileService.Features.OccasionFeatures.Add.AddOccasion.Dto;
using UserProfileService.Features.Shared;
using UserProfileService.Shared.Entities;
using UserProfileService.Shared.Hup;
using UserProfileService.Shared.Interfaces;

namespace UserProfileService.Features.OccasionFeatures.Commands.UpdateOccasion
{
    public record UpdateOccasionCommand(Guid Occasionid, string Name, bool Status) : IRequest<Result<bool>>;

    public class UpdateOccasionCommandHandler : IRequestHandler<UpdateOccasionCommand, Result<bool>>
    {
        private readonly IRepository<Occasion> repository;
        private readonly IHubContext<OccasionHub> hub;

        public UpdateOccasionCommandHandler(IRepository<Occasion> repository, IHubContext<OccasionHub> hub)
        {
            this.repository = repository;
            this.hub = hub;
        }
        public async Task<Result<bool>> Handle(UpdateOccasionCommand request, CancellationToken cancellationToken)
        {
            var occasion = await repository.GetByIdAsync(request.Occasionid,cancellationToken);

            if (occasion == null )
            {
                return Result<bool>.FailResponse("Occasion not found");
            }
            var  isNameExists = await repository.ExistsAsync(o => o.Name == occasion.Name && o.Id != occasion.Id, cancellationToken);

            if (isNameExists)
            {
                return Result<bool>.FailResponse("Occasion name already exists");
            };
            var newoccasion = new Occasion ()
            { Name= request.Name,
               Status= request.Status
            };
            repository.SaveInclude(occasion,
                 nameof(occasion.Name),
                   nameof(occasion.Status));

            await repository.SaveChangesAsync();
            await hub.Clients.All.SendAsync("ReceiveOccasionUpdate", new OccasionRequest
            {
                Name = occasion.Name,
                Status = occasion.Status,
            });


            return Result<bool>.SuccessResponse(true);
        }
    }


}
