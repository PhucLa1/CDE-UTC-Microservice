namespace Project.Infrastructure.Data.Configurations
{
    public class FilePermissionConfiguration : IEntityTypeConfiguration<FilePermission>
    {
        public void Configure(EntityTypeBuilder<FilePermission> builder)
        {
            builder.HasOne(fp => fp.File)
                .WithMany(f => f.FilePermissions)
                .HasForeignKey(fp => fp.FileId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
