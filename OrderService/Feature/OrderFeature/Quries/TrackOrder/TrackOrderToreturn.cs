    namespace OrderService.Feature.OrderFeature.Quries.TrackOrder
    {
        public class TrackOrderToreturn
        {
            public Guid OrderId { get; set; }
            public string OrderNumber { get; set; }
            public string Status { get; set; }

            public double? CurrentLat { get; set; }
            public double? CurrentLng { get; set; }

            public DateTime? EstimatedArrivalTime { get; set; }

            public string? DeliveryHeroName { get; set; }
            public string? DeliveryHeroContact { get; set; }

            public string ShippingAddress { get; set; }
            public decimal SubTotal { get; set; }
            public decimal DeliveryFee { get; set; }
            public decimal TotalAmount { get; set; }

            public IEnumerable<TrackOrderItemDto> Items { get; set; } = new HashSet<TrackOrderItemDto>();
        }
    }