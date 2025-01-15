

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
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Tag>()
                .WithMany()
                .HasForeignKey(o => o.TagId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            // Chuyển từ Table SQL -> Giá trị ValueObject
        }
    }
}
