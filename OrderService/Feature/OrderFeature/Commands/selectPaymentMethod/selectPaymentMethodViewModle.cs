using OrderService.Shared.Entites;

namespace OrderService.Feature.OrderFeature.Commands.selectPaymentMethod
{
    public class selectPaymentMethodViewModle
    {
       public Guid orderid { get; set; }
       public PaymentMethods PaymentMethods { get; set; }

    }
}
