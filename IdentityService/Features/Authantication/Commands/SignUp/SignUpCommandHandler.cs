using IdentityService.Shared.Entites;
using IdentityService.Shared.Enums;
using IdentityService.Shared.Repository;
using IdentityService.Shared.Respones;
using IdentityService.Shared.Services;
using IdentityService.Shared.Services.MessageBroker.Messages;
using MassTransit;
using MediatR;

namespace IdentityService.Features.Authantication.Commands.SignUp
{
    public class SignUpCommandHandler(IGenericRepository<Shared.Entites.User> _repository , IAuthService _authService , IPublishEndpoint _publishEndPoint) : IRequestHandler<SignUpCommand, ResponseResult<string>> 
    {
        public async Task<ResponseResult<string>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {

            var EmailIsExist =await _repository.ExistsAsync(u => u.Email == request.Email);
            if (EmailIsExist)
                return ResponseResult<string>.FailResponse("Email is already in use", statusCode: 409);
            
            if (request.Password != request.ConfirmPassword)
                return ResponseResult<string>.FailResponse("Password and Confirm Password do not match", statusCode: 400);

            Gender gender = Enum.TryParse(request.Gender, true, out gender) ? gender : Gender.Other;

            var hashedPassword =await _authService.GeneratePasswordHashAsync(request.Password);

            var user = new Shared.Entites.User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = hashedPassword,
                PhoneNumber = request.PhoneNumber,
                Gender = gender,
                UserRoles = new List<UserRole>()
            };
            user.UserRoles.Add(new UserRole { RoleId = RoleType.Customer });
            await _repository.AddAsync(user);
            var UserCreatedEvent = new UserCreatedEvent
            {
                UserId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender.ToString(),
                CreatedAt = DateTime.UtcNow
            };
            await _publishEndPoint.Publish(UserCreatedEvent, cancellationToken);
            return ResponseResult<string>.SuccessResponse(user.Email, "User registered successfully", statusCode: 201);
    
        }
    }
}
