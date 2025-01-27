

namespace Project.Infrastructure.Data.Configurations
{
    public class FileTagConfiguration : IEntityTypeConfiguration<FileTag>
    {
        public void Configure(EntityTypeBuilder<FileTag> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                FileTagId => FileTagId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => FileTagId.Of(dbId));
            
            builder.HasOne<File>()
                .WithMany()
                .HasForeignKey(o => o.FileId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne<Tag>()
                .WithMany()
                .HasForeignKey(o => o.TagId)
                .OnDelete(DeleteBehavior.SetNull);
            // Chuyển từ Table SQL -> Giá trị ValueObject
        }
    }
}
