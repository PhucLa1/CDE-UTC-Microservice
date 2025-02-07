namespace Project.Infrastructure.Data.Configurations
{
    public class FolderPermissionConfiguration : IEntityTypeConfiguration<FolderPermission>
    {
        public void Configure(EntityTypeBuilder<FolderPermission> builder)
        {
            builder.HasOne<Folder>()
                .WithMany()
                .HasForeignKey(o => o.FolderId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
