namespace Project.Application.Features.Groups.AddUserGroup
{
    public class AddUserGroupRequest : ICommand<AddUserGroupResponse>
    {
        public List<int> UserIds { get; set; } = new List<int>();
        public int GroupId { get; set; }
        public int ProjectId { get; set; }
    }
}
