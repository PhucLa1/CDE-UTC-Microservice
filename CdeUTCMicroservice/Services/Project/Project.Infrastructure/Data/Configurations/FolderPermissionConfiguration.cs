namespace Project.Infrastructure.Data.Configurations
{
    public class FolderPermissionConfiguration : IEntityTypeConfiguration<FolderPermission>
    {
        public void Configure(EntityTypeBuilder<FolderPermission> builder)
        {
            builder.HasOne(fp => fp.Folder)
                .WithMany(f => f.FolderPermissions)
                .HasForeignKey(fp => fp.FolderId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
