using Microsoft.AspNetCore.SignalR;
using ProductCatalogService.Features.Occasion.Add.AddOccasion.Dto;

namespace ProductCatalogService.Shared.Hup
{
    public class OccasionHub:Hub
    {
        public async Task SendOccasion(OccasionRequest occasion)
        {
            await Clients.All.SendAsync("ReceiveOccasionAdd", occasion);
        }
    }
}
