using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Feature.OrderFeature.Commands.CreateOrder;
using OrderService.Feature.OrderIemsFeature.Command.AddOrderItems;
using OrderService.Respones;
using OrderService.Shared.Entites;
using OrderService.Shared.Interface;
using OrderService.Shared.Repository;

namespace OrderService.Feature.OrderFeature.Commands.selectPaymentMethod
{
    public record selectPaymentMethodCommand(Guid orderid,PaymentMethods PaymentMethods): ICommand<RequestRespones<OrderToReturnDto>>;

    public class selectPaymentMethodCommandHandler : IRequestHandler<selectPaymentMethodCommand, RequestRespones<OrderToReturnDto>>
    {
        private readonly IGenericRepository<Order> genericRepository;

        public selectPaymentMethodCommandHandler(IGenericRepository<Order> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public async Task<RequestRespones<OrderToReturnDto>> Handle(selectPaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var order = await genericRepository.GetQueryableByCriteria(o => o.Id == request.orderid && o.Status == OrderStatus.Preparing).Include(x=>x.OrderItems)
                .FirstOrDefaultAsync(cancellationToken);
            if (order==null)
            {
                return RequestRespones<OrderToReturnDto>.Fail("Order not found",404);
            }
            order.PaymentMethod = request.PaymentMethods;
            await genericRepository.SaveChangesAsync();
            var orderToReturn = new OrderToReturnDto
            {
                Id = order.Id,
                UserId=order.UserId,
                CurrentLat = order.CurrentLat,
                CurrentLng=order.CurrentLng,
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

                OrderItems= order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductName = oi.ProductName,
                    Price = oi.Price,
                    Quantity = oi.Quantity
                }).ToList()

            };
            return RequestRespones<OrderToReturnDto>.Success(orderToReturn);

        }
    }
}
