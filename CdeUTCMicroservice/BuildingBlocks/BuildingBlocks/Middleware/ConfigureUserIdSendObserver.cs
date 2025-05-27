using MassTransit;
using Microsoft.AspNetCore.Http;
namespace BuildingBlocks.Middleware
{
    public class ConfigureUserIdSendObserver : IPublishObserver, ISendObserver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConfigureUserIdSendObserver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task PostPublish<T>(PublishContext<T> context) where T : class => Task.CompletedTask;

        public Task PrePublish<T>(PublishContext<T> context) where T : class
        {
            SetUserIdHeader(context);
            return Task.CompletedTask;
        }

        public Task PublishFault<T>(PublishContext<T> context, Exception exception) where T : class => Task.CompletedTask;

        public Task PostSend<T>(SendContext<T> context) where T : class => Task.CompletedTask;

        public Task PreSend<T>(SendContext<T> context)
            where T : class
        {
            SetUserIdHeader(context);
            return Task.CompletedTask;
        }

        public Task SendFault<T>(SendContext<T> context, Exception exception) where T : class => Task.CompletedTask;

        private void SetUserIdHeader(SendContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null &&
                httpContext.Request.Headers.TryGetValue("X-UserId", out var userIdValues) &&
                int.TryParse(userIdValues.FirstOrDefault(), out var userId))
            {
                context.Headers.Set("UserId", userId);
            }
        }
    }

}
