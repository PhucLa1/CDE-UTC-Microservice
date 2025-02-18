namespace Project.Application.Features.Groups.DeleteUserGroup
{
    public class DeleteUserGroupRequest : ICommand<DeleteUserGroupResponse>
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int ProjectId { get; set; }
    }
}
