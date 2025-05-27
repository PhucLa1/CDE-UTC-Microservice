using MassTransit;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Middleware
{
    public class UserIdSendFilter<T> : IFilter<SendContext<T>> where T : class
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdSendFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null &&
                httpContext.Request.Headers.TryGetValue("X-UserId", out var userIdValues) &&
                int.TryParse(userIdValues.FirstOrDefault(), out var userId))
            {
                context.Headers.Set("UserId", userId);
            }

            return next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateFilterScope("UserIdSendFilter");
        }
    }

}
