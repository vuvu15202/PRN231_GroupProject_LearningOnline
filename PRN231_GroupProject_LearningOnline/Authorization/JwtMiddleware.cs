namespace PRN231_GroupProject_LearningOnline.Authorization;

using Microsoft.Extensions.Options;
using PRN231_GroupProject_LearningOnline.Helpers;
using PRN231_GroupProject_LearningOnline.Services;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
    {
        string token;
        token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        //use for view
        if(token == null || string.IsNullOrEmpty(token))
        {
            if (context.Request.Cookies["token"] != null)
            {
                token = context.Request.Cookies["token"];
            }
        }
        //
        var userId = jwtUtils.ValidateJwtToken(token);
        if (userId != null)
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = userService.GetById(userId.Value);
        }

        await _next(context);
    }
}