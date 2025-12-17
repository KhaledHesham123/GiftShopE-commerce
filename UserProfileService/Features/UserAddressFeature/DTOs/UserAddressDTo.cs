namespace UserProfileService.Features.UserAddressFeature.DTOs
{
    public class UserAddressDTo
    {
        public string RecipientName { get; set; }
        public string PhoneNumber { get; set; }


        public string Governorate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public string BuildingNameOrNo { get; set; }
        public string FloorNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string? Landmark { get; set; }
    }
}
