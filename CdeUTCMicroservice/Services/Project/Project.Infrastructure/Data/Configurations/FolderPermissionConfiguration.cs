
namespace Project.Infrastructure.Data.Configurations
{
    public class FolderPermissionConfiguration : IEntityTypeConfiguration<FolderPermission>
    {
        public void Configure(EntityTypeBuilder<FolderPermission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                FolderPermissionId => FolderPermissionId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => FolderPermissionId.Of(dbId));
            // Chuyển từ Table SQL -> Giá trị ValueObject
            builder.HasOne<Folder>()
                .WithMany()
                .HasForeignKey(o => o.FolderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
