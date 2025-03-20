namespace UserManagementAPI.Middleware
{
    public class LoggingMiddleware
    {
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log Request
        var method = context.Request.Method;
        var path = context.Request.Path;

        await _next(context);

        // Log Response
        var statusCode = context.Response.StatusCode;
        Console.WriteLine($"HTTP {method} {path} responded with {statusCode}");
    }
}
}
