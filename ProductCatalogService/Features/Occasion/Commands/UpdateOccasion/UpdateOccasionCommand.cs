using MediatR;
using ProductCatalogService.Features.CategoryFeatures.Commands.UpdateCategory;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Interfaces;

namespace ProductCatalogService.Features.Occasion.Commands.UpdateOccasion
{
    public record UpdateOccasionCommand(Guid Occasionid, string Name, bool Status) : IRequest<Result<bool>>;

    public class UpdateOccasionCommandHandler : IRequestHandler<UpdateOccasionCommand, Result<bool>>
    {
        private readonly IRepository<Occasion> repository;

        public UpdateOccasionCommandHandler(IRepository<Occasion> repository)
        {
            this.repository = repository;
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

            return Result<bool>.SuccessResponse(true);
        }
    }


}
