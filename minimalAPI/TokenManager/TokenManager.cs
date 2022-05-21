using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using minimalAPI.Models;

namespace minimalAPI.TokenManager
{
    public interface ITokenManager
    {
        Task RevokeCurrentJWT();

        Task<bool> IsValidJWT();
    }
    public class TokenManager : ITokenManager
    {
        private readonly IDistributedCache cache;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IOptions<JWTSettings> options;

        public TokenManager(IDistributedCache cache, IHttpContextAccessor httpContextAccessor,IOptions<JWTSettings> options)
        {
            this.cache = cache;
            this.httpContextAccessor = httpContextAccessor;
            this.options = options;
        }

        public async Task<bool> IsValidJWT()
        {
            var token = GetJWT();
            return await cache.GetStringAsync(GetKey(token)) == null;

        }

        public async Task RevokeCurrentJWT()
        {
            var token = GetJWT();
            var key = GetKey(token);
            await cache.SetStringAsync(key, "", new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(options.Value.ExpiryMinutes)
            });
        }


        //helper method to help get the JWT string

        private string GetJWT()
        {
            var authorizationHeader = httpContextAccessor
           .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
          ? string.Empty
          : authorizationHeader.Single().Split(" ").Last();
        }

        private static string GetKey(string token)
      => $"tokens:{token}:deactivated";
    }
}
