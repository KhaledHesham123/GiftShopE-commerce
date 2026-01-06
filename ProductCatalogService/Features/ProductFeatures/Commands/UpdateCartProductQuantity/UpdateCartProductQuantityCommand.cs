using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Respones;
using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Interfaces;
using ProductCatalogService.Shared.Repositories;
using System.Net.Http;
using System.Text.Json;
using static MassTransit.ValidationResultExtensions;

namespace ProductCatalogService.Features.ProductFeatures.Commands.UpdateCartProductQuantity
{
    public record UpdateCartProductQuantityCommand(Guid userid, int newQuantity, Guid Productid) :IRequest<Result<bool>>;

    public class UpdateCartProductQuantityCommandHandler : IRequestHandler<UpdateCartProductQuantityCommand, Result<bool>>
    {
        private readonly IRepository<Product> repository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration configuration;

        public UpdateCartProductQuantityCommandHandler(IRepository<Product> repository, IHttpClientFactory httpClientFactory,IConfiguration configuration)
        {
            this.repository = repository;
            this._httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }
        public async Task<Result<bool>> Handle(UpdateCartProductQuantityCommand request, CancellationToken cancellationToken)
        {
            var Product = await repository.GetQueryableByCriteria(x => x.Id == request.Productid).FirstOrDefaultAsync();

            if (Product==null)
            {
                return Result<bool>.FailResponse("this product was not found");

            }

            var isUpdated = await UpdatePeoductQuantaty(Product.Id, request.userid, request.newQuantity);

            if (!isUpdated)
                return Result<bool>.FailResponse("Error while updating product quantity in cart service");
            return Result<bool>.SuccessResponse(true);

        }

        private async Task<bool> UpdatePeoductQuantaty(Guid productid,Guid userid, int newQuantaty) 
        {
            var httpclient = _httpClientFactory.CreateClient("CartService");

            try
            {
                var response = await httpclient.PostAsJsonAsync("api/Cart/updateProductQuantityInUserBasket", new
                {
                    Basketid = userid,
                    ProductId = productid,
                    newQuantity = newQuantaty
                });

                if (!response.IsSuccessStatusCode) return false;

                var result = await response.Content.ReadFromJsonAsync<EndpointRespones<bool>>();
                return result?.IsSuccess ?? false;
            }
            catch (HttpRequestException) 
            {
                return false;
            }


        }
    }
}
