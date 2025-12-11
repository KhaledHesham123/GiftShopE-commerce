using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ProductCatalogService.Features.OccasionFeatures.Add.AddOccasion.Dto;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Features.Shared.Queries.CheckExist;
using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Hup;


//using ProductCatalogService.Features.Shared.Queries.CheckExist;
using ProductCatalogService.Shared.Interfaces;

namespace ProductCatalogService.Features.OccasionFeatures.Add.AddOccasionQr
{
    public class AddOccasionQr : IAddOccasionQr
    {
        private readonly IHubContext<OccasionHub> _hub;
        private readonly IMediator _mediator;
        private readonly IImageHelper _image;

        public  AddOccasionQr(IHubContext<OccasionHub> hub, IMediator mediator, IImageHelper image)
        {
            _hub = hub;
            _mediator= mediator;
            _image = image;
        }

        public async Task<Result<bool>> IAddOccasion(OccasionRequest Request)
        {
            var flag = await _mediator.Send(new CheckExistQuery<Occasion>(e => e.Name == Request.Name.ToLower()));
            if (string.IsNullOrEmpty(Request.Name) || flag.Success)
                return Result<bool>.FailResponse("Occasion already exists");
            string imageUrl = string.Empty;
            if (null != Request.ImageUrl ) 
            {
                imageUrl = await _image.SaveImageAsync(Request.ImageUrl, "Occasion");
            }
            var flag2 =  await _mediator.Send(new OccasionFeatures.Add.AddOccasion.AddOccasion(Request.Name.ToLower(), Request.Status, imageUrl));
            if (!flag2.Success)
                return Result<bool>.FailResponse("Failed to add occasion");
            //hub 
            if(Request.Status==true)
                 await _hub.Clients.All.SendAsync("ReceiveOccasionAdd", new OccasionDro
            {
                 Id= flag2.Data.Id,
                Name = Request.Name,
                Status = Request.Status,
                ImageUrl = imageUrl
            });


            return Result<bool>.SuccessResponse(flag2.Success, "Occasion added successfully", 201);

        }
    }
}
