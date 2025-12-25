namespace OrderService.Feature.OrderFeature.Commands.UpdateOrderStatus.DTOs
{
    public class DeliveryLocationDto
    {
        public string? HeroName { get; set; }
        public string? HeroContact { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public DateTime? EstimatedArrival { get; set; }
    }
}