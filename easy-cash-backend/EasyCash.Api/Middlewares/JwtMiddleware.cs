
using System.Text;
using EasyCash.Domain;
using EasyCash.Application.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace EasyCash.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, AppSettings appSettings)
    {
        _next = next;
        _appSettings = appSettings;
    }

    public async Task Invoke(HttpContext context, IUserRepository userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            await AttachUserToContext(context, userService, token);

        await _next(context);
    }

    private async Task AttachUserToContext(HttpContext context, IUserRepository userRepo, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            // attach user to context on successful jwt validation
            context.Items["User"] = await userRepo.GetByIdAsync(userId);
        }
        catch
        {
        }
    }
}