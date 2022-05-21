using System.Net;

namespace minimalAPI.TokenManager
{
    public class TokenManagerMiddleware : IMiddleware
    {
        private readonly ITokenManager tokenManager;

        public TokenManagerMiddleware(ITokenManager tokenManager)
        {
            this.tokenManager = tokenManager;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if(await tokenManager.IsValidJWT())
            {
                await next(context);
                return;
            }

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
