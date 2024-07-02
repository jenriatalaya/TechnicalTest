namespace TechnicalTest.Api.Infrastracture.Middlewares;

public class OrganizationResolverMiddleware
{
    private readonly RequestDelegate _next;

    public OrganizationResolverMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var pathSegments = context.Request.Path.Value.Split('/');

        if (pathSegments.Length > 1)
        {
            var slugTenant = pathSegments[1];
            context.Items["slugTenant"] = slugTenant;
        }

        await _next(context);
    }
}
