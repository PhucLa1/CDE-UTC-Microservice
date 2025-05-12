namespace YarpApiGateway.Middleware
{
    public class ProjectIdValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ProjectIdValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("X-Project-Id"))
            {
                //context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Missing X-Project-Id header.");
                await _next(context);
            }
            await _next(context);
        }
    }
}
