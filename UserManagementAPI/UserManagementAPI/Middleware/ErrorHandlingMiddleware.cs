namespace UserManagementAPI.Middleware
{
    public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log the exception (you could use a logging framework here)
            Console.WriteLine($"Unhandled Exception: {ex.Message}");

            // Return a consistent JSON error response
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var errorResponse = new { error = "Internal server error." };
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
}