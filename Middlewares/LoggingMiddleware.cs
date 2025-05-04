namespace UserManagementApi.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        _logger.LogInformation($"""
                                Request Method: {httpContext.Request.Method}
                                Request Path: {httpContext.Request.Path}
                                """);

        await _next.Invoke(httpContext);

        _logger.LogInformation($"Response StatusCode: {httpContext.Response.StatusCode}");
    }
}