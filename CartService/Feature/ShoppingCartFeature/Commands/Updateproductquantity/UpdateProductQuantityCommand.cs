using CartService.Shared.basketRepository;
using CartService.Shared.Respones;
using MediatR;

namespace CartService.Feature.ShoppingCartFeature.Commands.Updateproductquantity
{
    public record UpdateProductQuantityCommand(Guid Basketid,int newQuantity,Guid Productid):IRequest<RequestRespones<bool>>;

    public class UpdateProductQuantityCommandHandler : IRequestHandler<UpdateProductQuantityCommand, RequestRespones<bool>>
    {
        private readonly IbasketRepository basketRepository;

        public UpdateProductQuantityCommandHandler(IbasketRepository ibasketRepository )
        {
            this.basketRepository = ibasketRepository;
        }
        public async Task<RequestRespones<bool>> Handle(UpdateProductQuantityCommand request, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetCustomerBasket(request.Basketid.ToString());

            if (basket == null)
            {
                return RequestRespones<bool>.Fail("There is no basket for this user", 404);
            }

            var itemToUpdate = basket.Items.FirstOrDefault(x => x.ProductId == request.Productid);

            if (itemToUpdate == null)
            {
                return RequestRespones<bool>.Fail("Product not found in basket", 404);
            }

            if (request.newQuantity <= 0)
            {
                basket.Items = basket.Items.Where(x => x.ProductId != request.Productid).ToList();
            }
            else
            {
                itemToUpdate.Quantity = request.newQuantity;
            }

          

            var result= await basketRepository.UpdateOrCustomerBasket(basket);

            if (result==null)
            {
                return RequestRespones<bool>.Fail("Failed to update basket", 500);
            }

            return RequestRespones<bool>.Success(true, "Quantity updated successfully");


        }
    }
}
