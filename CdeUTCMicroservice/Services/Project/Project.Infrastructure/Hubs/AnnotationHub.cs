using Microsoft.AspNetCore.SignalR;
using Project.Application.Hubs;

namespace Project.Infrastructure.Hubs
{
    public class AnnotationHub
        (IHubContext<AnnotationHub> hubContext) : Hub, IAnnotationHub
    {
        public async Task JoinChannel(int viewId)
        {
            await hubContext.Groups.AddToGroupAsync(Context.ConnectionId, viewId.ToString());
        }

        // Rời channel khi cần
        public async Task LeaveChannel(int viewId)
        {
            await hubContext.Groups.RemoveFromGroupAsync(Context.ConnectionId, viewId.ToString());
        }

        // Gửi annotation tới channel cụ thể
        public async Task SendAnnotationToChannel(int viewId, string annotation, int action)
        {
            try
            {
                await hubContext.Clients.Group(viewId.ToString()).SendAsync("ReceiveAnnotation", viewId, annotation, action);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
