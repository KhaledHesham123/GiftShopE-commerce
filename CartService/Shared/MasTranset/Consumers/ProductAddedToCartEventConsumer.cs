using CartService.Feature.ShoppingCartFeature.Commands.addtProducToCart;
using MassTransit;
using MediatR;

namespace CartService.Shared.MasTranset.Consumers
{
    public class ProductAddedToCartEventConsumer : IConsumer<ProductAddedToCartEvent>
    {
        private readonly IMediator mediator;
        private readonly ILogger<ProductAddedToCartEventConsumer> logger;

        public ProductAddedToCartEventConsumer(IMediator mediator,ILogger<ProductAddedToCartEventConsumer> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }
        public async Task Consume(ConsumeContext<ProductAddedToCartEvent> context)
        {
            var message = context.Message;

            if ( message.UserId == Guid.Empty)
            {
                logger.LogError("Invalid ProductAddedToCartEvent received: {Message}", message);
                return;
            }

            var addProductToCartReslut = await mediator.Send(new addProducToCartCommand
                (ShoppingCartId: message.UserId.ToString(),
                userid: message.UserId,
                ProductId: message.ProductId,
                ProductName: message.Name,
                ProductPrice: message.Price,
                ProductImageUrl: message.ProductImageUrl,
                Quantity: message.Quantity));

            if (!addProductToCartReslut.Success)
            {
                throw new Exception(addProductToCartReslut.Message);


            }





        }
    }
}
