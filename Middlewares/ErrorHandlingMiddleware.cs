using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace UserManagementApi.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            httpContext.Response.ContentType = "application/json";

            var errorResObj = new { errorMessage = ex.Message, statusCode = httpContext.Response.StatusCode };
            
            var errorResJson = JsonSerializer.Serialize(errorResObj);

            await httpContext.Response.WriteAsync(errorResJson);
        }
    }
}