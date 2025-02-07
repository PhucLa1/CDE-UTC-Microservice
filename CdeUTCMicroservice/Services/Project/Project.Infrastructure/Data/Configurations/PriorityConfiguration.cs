namespace Project.Infrastructure.Data.Configurations
{
    public class PriorityConfiguration : IEntityTypeConfiguration<Priority>
    {
        public void Configure(EntityTypeBuilder<Priority> builder)
        {
            builder.HasOne(x => x.Project)
                .WithMany(x => x.Priorities)
                .HasForeignKey(o => o.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
