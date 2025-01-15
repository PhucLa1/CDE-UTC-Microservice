
using Project.Domain.Abstractions;

namespace Project.Infrastructure.Data.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                FileId => FileId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => FileId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.Property(x => x.FolderId).HasConversion(
                FolderId => FolderId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => FolderId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.Property(x => x.ProjectId).HasConversion(
                ProjectId => ProjectId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => ProjectId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject
            builder.Property(f => f.Size)
                .HasPrecision(18, 4);
        }
    }
}
