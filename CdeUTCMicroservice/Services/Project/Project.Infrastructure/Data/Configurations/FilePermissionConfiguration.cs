namespace Project.Infrastructure.Data.Configurations
{
    public class FilePermissionConfiguration : IEntityTypeConfiguration<FilePermission>
    {
        public void Configure(EntityTypeBuilder<FilePermission> builder)
        {
            builder.HasOne<File>()
                .WithMany()
                .HasForeignKey(o => o.FileId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
