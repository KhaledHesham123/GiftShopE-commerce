using System.ComponentModel.DataAnnotations;

namespace ProductCatalogService.Shared.Entities
{
    public class UserAddress :BaseEntity
    {
        
        public Guid UserProfileId { get; set; }

        public string RecipientName { get; set; }
        public string PhoneNumber { get; set; }

        
        public string Governorate { get; set; } 
        public string City { get; set; }
        public string Street { get; set; }

        public string BuildingNameOrNo { get; set; } 
        public string FloorNumber { get; set; }      
        public string ApartmentNumber { get; set; } 
        public string? Landmark { get; set; }       

        
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsDefault { get; set; }
    }
}
