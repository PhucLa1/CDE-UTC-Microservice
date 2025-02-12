namespace Project.Application.Features.Groups.CreateGroup
{
    public class CreateGroupRequest : ICommand<CreateGroupResponse>
    {
        public int ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
