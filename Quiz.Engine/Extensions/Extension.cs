using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Quiz.Engine.Service;
using Quiz.Engine.Utils.Settings;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Quiz.Engine.Extensions;

public static class Extension
{
    public static T GetOptions<T>(this IConfiguration configuration) where T : new()
    {
        var t = new T();
        configuration.GetSection(typeof(T).Name).Bind(t);
        return t;
    }

    public static void RegisterJWTAuthentication(this IServiceCollection services, JWTSettings settings)
    {
        var key = Encoding.ASCII.GetBytes(settings.SecretKey);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }

    public static Guid GetUserId(this IPrincipal claimsPrincipal)
    {
        var identity = claimsPrincipal.Identity as ClaimsIdentity;
        var userId = identity?.FindFirst(ClaimTypes.SerialNumber)?.Value;
        if (userId is null)
        {
            throw new SomethingWentWrongException();
        }

        return new Guid(userId);
    }

    public static bool IsRefreshToken(this IPrincipal claimsPrincipal)
    {
        var identity = claimsPrincipal.Identity as ClaimsIdentity;
        var isRefreshToken = identity?.FindFirst(ClaimTypes.AuthorizationDecision)?.Value;
        return isRefreshToken == "RefreshToken";
    }
}
