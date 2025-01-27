namespace Project.Domain.Entities
{
    public class Group : Entity<GroupId>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public ProjectId? ProjectId { get; set; }
       
    }
}
