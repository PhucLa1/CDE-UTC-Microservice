namespace Project.Infrastructure.Data.Configurations
{
    public class FolderConfiguration : IEntityTypeConfiguration<Folder>
    {
        public void Configure(EntityTypeBuilder<Folder> builder)
        {
            builder.HasOne<Projects>()
                .WithMany()
                .HasForeignKey(o => o.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(o => o.FullPath).IsUnique();
            builder.HasIndex(o => o.FullPathName).IsUnique();
        }
    }
}
