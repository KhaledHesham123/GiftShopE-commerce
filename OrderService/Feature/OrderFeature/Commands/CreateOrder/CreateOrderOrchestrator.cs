using MediatR;
using OrderService.Feature.OrderIemsFeature.Command.AddOrderItems;
using OrderService.Respones;
using OrderService.Shared;
using OrderService.Shared.Entites;
using OrderService.Shared.Interface;
using OrderService.Shared.Repository;
using ProductCatalogService.Shared.basketRepository;

namespace OrderService.Feature.OrderFeature.Commands.CreateOrder
{
    public record CreateOrderOrchestrator(
    Guid UserId,
    string ShippingAddress,
    string RecipientName,
    string RecipientPhone,
    PaymentMethods PaymentMethod,
    int PointsRedeemed,
    double? CurrentLat,
    double? CurrentLng): ICommand<RequestRespones<OrderToReturnDto>>;

    public class CreateOrderOrchestratorHandler:IRequestHandler<CreateOrderOrchestrator,RequestRespones<OrderToReturnDto>>
    {
        private readonly IGenericRepository<Order> genericRepository;
        private readonly IbasketRepository basketRepository;

        public CreateOrderOrchestratorHandler(IGenericRepository<Order> genericRepository,IbasketRepository basketRepository)
        {
            this.genericRepository = genericRepository;
            this.basketRepository = basketRepository;
        }
        public async Task<RequestRespones<OrderToReturnDto>> Handle(CreateOrderOrchestrator request, CancellationToken cancellationToken)
        {
            
            var basket = await basketRepository.GetCustomerBasket(request.UserId.ToString());

            if (basket == null || basket.Items.Count == 0)
            {
                return RequestRespones<OrderToReturnDto>.Fail(" User Basket is empty or not found", 400);
            }

            var orderItems = basket.Items.Select(item => new OrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = item.ProductId,
                Price = item.Price,
                Quantity = item.Quantity,
                ProductName = item.ProductName
            }).ToList();

            var order= new Order
            {
                Id=Guid.NewGuid(),
                UserId = request.UserId,
                ShippingAddress = request.ShippingAddress,
                RecipientName = request.RecipientName,
                RecipientPhone = request.RecipientPhone,
                PaymentMethod = request.PaymentMethod,
                PointsRedeemed = request.PointsRedeemed,
                CurrentLat = request.CurrentLat,
                CurrentLng = request.CurrentLng,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Preparing,
                OrderNumber = GenerateOrderNumber(),
                SubTotal = basket.Items.Sum(item => item.Quantity * item.Price),
                OrderItems = orderItems,
                

            };


            //var AddOrderResult= await mediator.Send(new CreateOrderCommand(order),cancellationToken);

           
                await genericRepository.AddAsync(order);

                var result = await genericRepository.SaveChangesAsync();

                if (result > 0)
                {
                    return RequestRespones<OrderToReturnDto>.Success(MaptoOrderToReturnDto(order));
                }

                return RequestRespones<OrderToReturnDto>.Fail("Failed to save the order to database.", 500);
          
        }
        public string GenerateOrderNumber()
        {
            return $"{Random.Shared.Next(1000, 10000)}";
        }

        private OrderToReturnDto MaptoOrderToReturnDto(Order order)
        {
            return new OrderToReturnDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CurrentLat = order.CurrentLat,
                CurrentLng = order.CurrentLng,
                DeliveryHeroContact = order.DeliveryHeroContact,
                DeliveryHeroName = order.DeliveryHeroName,
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                ShippingAddress = order.ShippingAddress,
                RecipientName = order.RecipientName,
                RecipientPhone = order.RecipientPhone,
                SubTotal = order.SubTotal,
                DeliveryFee = order.DeliveryFee,
                PointsRedeemed = order.PointsRedeemed,
                PaymentMethod = order.PaymentMethod.ToString(),
                Status = order.Status.ToString(),
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductName = oi.ProductName,
                    Price = oi.Price,
                    Quantity = oi.Quantity,
                    
                }).ToList()
            };
        }


    }

   

}
