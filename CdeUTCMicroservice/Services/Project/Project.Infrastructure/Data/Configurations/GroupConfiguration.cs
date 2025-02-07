namespace Project.Infrastructure.Data.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasOne<Projects>()
                .WithMany()
                .HasForeignKey(o => o.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
