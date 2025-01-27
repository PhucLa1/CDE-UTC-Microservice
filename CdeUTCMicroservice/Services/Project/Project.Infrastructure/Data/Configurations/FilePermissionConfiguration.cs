
namespace Project.Infrastructure.Data.Configurations
{
    public class FilePermissionConfiguration : IEntityTypeConfiguration<FilePermission>
    {
        public void Configure(EntityTypeBuilder<FilePermission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                FilePermissionId => FilePermissionId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => FilePermissionId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            
            builder.HasOne<File>()
                .WithMany()
                .HasForeignKey(o => o.FileId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
