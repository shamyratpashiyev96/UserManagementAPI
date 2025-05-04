using Microsoft.AspNetCore.Authentication;

namespace UserManagementApi.Middlewares;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private static string AuthenticationToken = "";

    public AuthenticationMiddleware(
        RequestDelegate next,
        IConfiguration configuration)
    {
        _next = next;
        AuthenticationToken = configuration.GetValue<string>("StaticAuthToken");
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var tokenQuery = httpContext.Request.Query.Where(x => x.Key == "auth");

        if (!tokenQuery.Any())
        {
            await ModifyResponse(httpContext);
            throw new Exception("Unauthenticated. Invalid token.");
        }

        var token = tokenQuery.First();
        if (token.Value != AuthenticationToken)
        {
            await ModifyResponse(httpContext);
            throw new Exception("Unauthenticated. Invalid token.");
        }

        await _next(httpContext);
    }

    private async Task ModifyResponse(HttpContext httpContext)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized ;
    }
}