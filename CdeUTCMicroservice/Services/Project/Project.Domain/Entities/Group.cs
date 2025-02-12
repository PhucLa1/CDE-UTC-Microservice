namespace Project.Domain.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public int? ProjectId { get; set; }
        public List<UserGroup>? UserGroups { get; set; }

    }
}
