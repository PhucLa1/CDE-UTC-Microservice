
namespace Project.Infrastructure.Data.Configurations
{
    public class FolderConfiguration : IEntityTypeConfiguration<Folder>
    {
        public void Configure(EntityTypeBuilder<Folder> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                FolderId => FolderId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => FolderId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.Property(x => x.ParentId).HasConversion(
                ParentId => ParentId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => FolderId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.HasOne<Projects>()
                .WithMany()
                .HasForeignKey(o => o.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
