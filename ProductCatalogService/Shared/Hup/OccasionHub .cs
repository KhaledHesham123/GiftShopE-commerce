using Microsoft.AspNetCore.SignalR;
using ProductCatalogService.Features.OccasionFeatures.Add.AddOccasion.Dto;

namespace ProductCatalogService.Shared.Hup
{
    public class OccasionHub:Hub
    {
        public async Task SendOccasion(OccasionRequest occasion)
        {
            await Clients.All.SendAsync("ReceiveOccasionAdd", occasion);
        }


        public async Task SendOccasionUpdate(OccasionRequest occasion)
        {
            await Clients.All.SendAsync("ReceiveOccasionUpdate", occasion);
        }
    }
}
