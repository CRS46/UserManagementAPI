using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;


namespace UserManagementAPI.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Allow unauthenticated access to Swagger and Swagger JSON
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            await _next(context);
            return;
        }

        var authorizationHeader = context.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsJsonAsync(new { error = "Unauthorized access. Missing or invalid token." });
            return;
        }

        var token = authorizationHeader.Substring("Bearer ".Length).Trim();

        if (!ValidateToken(token))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsJsonAsync(new { error = "Unauthorized access. Invalid token." });
            return;
        }

        await _next(context);
    }

    private bool ValidateToken(string token)
    {
        return token == "valid-token"; // Placeholder validation logic
    }

    }
}