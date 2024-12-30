using Identity.Common.Helpers;
using Identity.DAL.Entities;
using Identity.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace Identity.API.Configuration
{
    /// <summary>
    /// Kiểm ra token liệu có bị hết hiệu lực do logout
    /// </summary>
    public class CustomAuthorize : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler defaultHandler = new();
        private readonly InvalidatedTokenRepository _invalidatedTokenRepository;
        private readonly AppSettings _appSettings;

        public CustomAuthorize(InvalidatedTokenRepository invalidatedTokenRepository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _invalidatedTokenRepository = invalidatedTokenRepository;
        }

        public async Task HandleAsync(
            RequestDelegate next,
            HttpContext context,
            AuthorizationPolicy policy,
            PolicyAuthorizationResult authorizeResult)
        {
            // If the authorization was forbidden and the resource had a specific requirement,
            // provide a custom 404 response.
            var jwtId = context.User.FindFirst(JwtRegisteredClaimNames.Jti)!.Value;
            var expiryTimeSql = context.User.FindFirst(JwtRegisteredClaimNames.Exp)!.Value;

            var dateExpiry = (DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiryTimeSql))
                .UtcDateTime);
            var jwt = await _invalidatedTokenRepository.GetAsync(jwtId);

            if (DateTime.UtcNow.CompareTo(dateExpiry.AddMinutes(_appSettings.RefreshTime)) < 0
                && jwt is not null)
            {
                // Return a 404 to make it appear as if the resource doesn't exist.
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var body = new
                {
                    message = "Token invalid - expiry is handled by CustomAuthorize"
                };

                await context.Response.WriteAsJsonAsync(body);
                return;
            }

            // Fall back to the default implementation.
            await defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }

}
