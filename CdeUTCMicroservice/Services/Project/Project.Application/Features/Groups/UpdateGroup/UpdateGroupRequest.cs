namespace Project.Application.Features.Groups.UpdateGroup
{
    public class UpdateGroupRequest : ICommand<UpdateGroupResponse>
    {
        public int ProjectId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
