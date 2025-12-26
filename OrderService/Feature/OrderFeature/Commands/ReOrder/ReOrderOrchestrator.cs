using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Feature.OrderFeature.Commands.CreateOrder;
using OrderService.Feature.OrderIemsFeature.Command.AddOrderItems;
using OrderService.Respones;
using OrderService.Shared.Entites;
using OrderService.Shared.Interface;
using OrderService.Shared.Repository;
using OrderService.Shared.SharedDto;
using System.Threading.Tasks;

namespace OrderService.Feature.OrderFeature.Commands.ReOrder
{
    public record ReOrderOrchestrator(Guid OrderId, string? NewShippingAddress, ICollection<ReOrderItemsOrderDto> Items) : ICommand<RequestRespones<OrderToReturnDto>>;

    public class ReOrderReOrderOrchestratorHandler : IRequestHandler<ReOrderOrchestrator, RequestRespones<OrderToReturnDto>>
    {
        private readonly IGenericRepository<Order> genericRepository;
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IMediator mediator;

        public ReOrderReOrderOrchestratorHandler(IMediator mediator, IGenericRepository<Order> genericRepository,IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.genericRepository = genericRepository;
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
            this.mediator = mediator;
        }
        public async Task<RequestRespones<OrderToReturnDto>> Handle(ReOrderOrchestrator request, CancellationToken cancellationToken)
        {
            var order = await genericRepository.GetQueryableByCriteria(x => x.Id == request.OrderId).FirstOrDefaultAsync(cancellationToken);
            if (order == null)
            {
                return RequestRespones<OrderToReturnDto>.Fail("there is no order with this id");
            }

            var productsIds = request.Items.Select(i => i.ProductId);

            var Products = await GETProductsByIds(productsIds);
            if (Products == null || !Products.Any())
            {
                return RequestRespones<OrderToReturnDto>.Fail("Could not retrieve product prices.");
            }

         

            var orderItemsToCreate = request.Items.Select(reqItem =>
            {
                var productDetail = Products.FirstOrDefault(p => p.ProductId == reqItem.ProductId);

                return new OrderItemDto
                {
                    id = reqItem.ProductId,
                    Quantity = reqItem.Quantity, 
                    Price = productDetail?.Price ?? 0, 
                    ProductName = productDetail?.ProductName ?? "Unknown"
                };
            }).ToList();

            decimal calculatedSubTotal = orderItemsToCreate.Sum(x => x.Price * x.Quantity);

            var NewOrder = new Order
            {
                Id = Guid.NewGuid(),
                OrderNumber = GenerateOrderNumber(),
                ShippingAddress = request.NewShippingAddress ?? order.ShippingAddress,

                RecipientName = order.RecipientName,
                RecipientPhone = order.RecipientPhone,

                Status = OrderStatus.Preparing,
                CurrentLat = order.CurrentLat,
                CurrentLng = order.CurrentLng,
                OrderDate = DateTime.UtcNow,
                DeliveryFee = order.DeliveryFee,
                PaymentMethod = order.PaymentMethod,
                UserId = order.UserId,
                SubTotal = calculatedSubTotal,
                PointsRedeemed = 0

            };

            var AddOrderResult = await mediator.Send(new CreateOrderCommand(NewOrder));

            if (!AddOrderResult.IsSuccess)
            {
                return RequestRespones<OrderToReturnDto>.Fail(AddOrderResult.Message??"Error while Adding Order");
            }


            var addOrderItemsResult = await mediator.Send( new AddOrderItemsCommand(orderItemsToCreate, NewOrder.Id));

            if (!addOrderItemsResult.IsSuccess)
            {
                return RequestRespones<OrderToReturnDto>.Fail(addOrderItemsResult .Message?? "error while adding OrderItems");
            }

            

            return RequestRespones<OrderToReturnDto>.Success(MaptoOrderToReturnDto(NewOrder));
        }


        private async Task<IEnumerable<ProductDTo?>> GETProductsByIds(IEnumerable<Guid> ProductsIds)
        {
            var httpclient = httpClientFactory.CreateClient("ProductService");
            var Products = new List<ProductDTo>();
            try
            {
                var response = await httpclient.PostAsJsonAsync("api/Products/GetProductsByIds", ProductsIds);

                if (response.IsSuccessStatusCode)
                {
                    var contnt = await response.Content.ReadFromJsonAsync<EndpointRespones<IEnumerable<ProductDTo>>>();

                    Products = contnt?.Data?.ToList() ?? new List<ProductDTo>();

                }

                return Products;
            }
            catch (Exception)
            {
                return null;

            }
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
                OrderNumber = order.OrderNumber,
                ShippingAddress = order.ShippingAddress,
                RecipientName = order.RecipientName,
                RecipientPhone = order.RecipientPhone,
                PaymentMethod = order.PaymentMethod.ToString(),
                PointsRedeemed = order.PointsRedeemed,
                CurrentLat = order.CurrentLat,
                CurrentLng = order.CurrentLng,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                SubTotal = order.SubTotal,
                DeliveryFee = order.DeliveryFee,
                TotalAmount = order.SubTotal + order.DeliveryFee - order.PointsRedeemed,
                OrderItems = order.OrderItems.Select(i => new OrderItemDto
                {
                    id = i.ProductId,
                    Price = i.Price,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity
                }).ToList()
            };
        }



    }
}
