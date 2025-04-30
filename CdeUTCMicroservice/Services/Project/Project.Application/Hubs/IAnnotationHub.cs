namespace Project.Application.Hubs
{
    public interface IAnnotationHub
    {
        Task SendAnnotationToChannel(int viewId, string annotation, int action);
        Task LeaveChannel(int viewId);
        Task JoinChannel(int viewId);

    }
}
