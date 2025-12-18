using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Interfaces;
using UserProfileService.Features.UserProfileFeature.DTOs;
using UserProfileService.Respones;

namespace UserProfileService.Features.UserProfileFeature.Quries.GetUserByid
{
    public record GetUserByidQuery(Guid userid):IRequest<RequestRespones<UserToReturnDto>>;

    public class GetUserByidQueryHandler : IRequestHandler<GetUserByidQuery, RequestRespones<UserToReturnDto>>
    {
        private readonly IRepository<UserProfile> repository;
        private readonly IMemoryCache memoryCache;

        public GetUserByidQueryHandler(IRepository<UserProfile> repository, IMemoryCache memoryCache)
        {
            this.repository = repository;
            this.memoryCache = memoryCache;
        }
        public async Task<RequestRespones<UserToReturnDto>> Handle(GetUserByidQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"UserProfile_{request.userid}";

            if (memoryCache.TryGetValue(cacheKey, out UserToReturnDto CachedUser))
            {
                return RequestRespones<UserToReturnDto>.Success(CachedUser);
            }

            var user = await repository.GetQueryableByCriteria(u => u.Id == request.userid)
                                         .Select(u => new UserToReturnDto
                                         {
                                             FirstName = u.FirstName,
                                             LastName = u.LastName,
                                             Gender=u.Gender,
                                             ProfileImageUrl=u.ProfileImageUrl,
                                             Addresses=u.Addresses.Select(a=> new Features.UserAddressFeature.DTOs.UserAddressDTo
                                             {
                                                 Street=a.Street,
                                                 City=a.City,
                                                 ApartmentNumber=a.ApartmentNumber,
                                                 BuildingNameOrNo=a.BuildingNameOrNo,
                                                    FloorNumber=a.FloorNumber,
                                                    Governorate=a.Governorate,
                                                    Landmark=a.Landmark,
                                                    PhoneNumber=a.PhoneNumber,
                                                    RecipientName=a.RecipientName
                                                  
                                             }).ToList()
                                         })
                                         .FirstOrDefaultAsync(cancellationToken);

            if (user == null)  return RequestRespones<UserToReturnDto>.Fail("User not found", 404);

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(10)) 
                .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                .SetPriority(CacheItemPriority.Normal);

            memoryCache.Set(cacheKey, user, cacheOptions);

            return RequestRespones<UserToReturnDto>.Success(user);
        }
    }
}
