namespace IdentityService.Features.UserToken.Quries.GetUserToken
{
    public class UserTokenToReturnDto
    {
        public Guid id { get; set; }
        public Guid UserId { get; set; }

        public string Token { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
