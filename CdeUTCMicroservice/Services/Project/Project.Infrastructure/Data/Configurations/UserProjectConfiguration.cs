namespace Project.Infrastructure.Data.Configurations
{
    public class UserProjectConfiguration : IEntityTypeConfiguration<UserProject>
    {
        public void Configure(EntityTypeBuilder<UserProject> builder)
        {
            builder.HasOne<Projects>()
                .WithMany()
                .HasForeignKey(o => o.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
