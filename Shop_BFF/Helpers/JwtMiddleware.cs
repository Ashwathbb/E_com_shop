using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Shop_BFF.Helpers
{
        public class JwtMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly AppSettings _appSettings;

            public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
            {
                _next = next;
                _appSettings = appSettings.Value;
            }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token) || !ValidateToken(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }

        private bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.Jwt.Key);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _appSettings.Jwt.Issuer,
                    ValidAudience = _appSettings.Jwt.Audience,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return validatedToken != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
