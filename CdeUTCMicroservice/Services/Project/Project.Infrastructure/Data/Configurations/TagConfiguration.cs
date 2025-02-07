namespace Project.Infrastructure.Data.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasOne<Projects>()
                .WithMany()
                .HasForeignKey(o => o.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
