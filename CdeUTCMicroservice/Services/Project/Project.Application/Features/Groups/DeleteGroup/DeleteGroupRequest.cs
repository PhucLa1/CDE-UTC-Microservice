namespace Project.Application.Features.Groups.DeleteGroup
{
    public class DeleteGroupRequest : ICommand<DeleteGroupResponse>
    {
        public int ProjectId { get; set; }
        public int Id { get; set; }
    }
}
