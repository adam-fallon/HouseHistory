
using System.Security.Claims;
using HouseHistory.Dependencies;

namespace HouseHistory.Middleware;
public class SupabaseSession
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;
    private readonly IServiceProvider _serviceProvider;

    public SupabaseSession(RequestDelegate next, IConfiguration config, IServiceProvider serviceProvider)
    {
        _next = next;
        _config = config;
        _serviceProvider = serviceProvider;
    }

    public async Task Invoke(HttpContext context)
    {
        var supabase = _serviceProvider.GetRequiredService<ISupabaseService>();
        var client = await supabase.GetClient();
        var accessToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var refreshToken = context.Request.Headers["X-RefreshToken"].ToString();

        if (accessToken == "" || refreshToken == "")
        {
            context.Response.StatusCode = 401;
            Console.WriteLine("No Token");
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        // Maybe user this later to redirect to auth pages?
        // var user = await client.Auth.GetUser(accessToken);
        // if (user == null)
        // {
        //     context.Response.StatusCode = 401;
        //     Console.WriteLine("No user found after Auth");
        //     await context.Response.WriteAsync("Unauthorized");
        //     return;
        // }

        await _next(context);
    }
}

public static class SupabaseSessionExtensions
{
    public static IApplicationBuilder UseSupabaseSession(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SupabaseSession>();
    }
}