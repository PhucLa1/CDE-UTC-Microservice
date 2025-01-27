
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
                FolderId => FolderId != null ? FolderId.Value : (Guid?)null, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => dbId.HasValue ? FolderId.Of(dbId.Value) : null // Chuyển từ Table SQL -> Giá trị ValueObject
            );

            builder.Property(x => x.ProjectId).HasConversion(
                ProjectId => ProjectId != null ? ProjectId.Value : (Guid?)null, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => dbId.HasValue ? ProjectId.Of(dbId.Value) : null // Chuyển từ Table SQL -> Giá trị ValueObject
            );
            builder.Property(f => f.Size)
                .HasPrecision(18, 4);
        }
    }
}
