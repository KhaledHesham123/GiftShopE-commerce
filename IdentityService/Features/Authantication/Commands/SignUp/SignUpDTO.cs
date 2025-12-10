namespace IdentityService.Features.Authantication.Commands.SignUp
{
    public class SignUpDTO
    {  
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; }= string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string ConfirmPassword { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public string Gender { get; set; } = string.Empty;
    }
}
