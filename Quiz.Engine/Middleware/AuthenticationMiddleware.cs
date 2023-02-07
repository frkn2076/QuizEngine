using Quiz.Engine.Extensions;
using Quiz.Engine.Service;

namespace Quiz.Engine.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string authHeader = context.Request.Headers["Authorization"];

        if (authHeader != null && context.Request.Path.Value?.ToLower() != "/authentication/refresh")
        {
            if (context.User.IsRefreshToken())
            {
                throw new RefreshTokenCanBeUsedForRefreshingTokenOnlyException();
            }
        }

        await _next(context);
    }
}
