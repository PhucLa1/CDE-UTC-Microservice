


namespace Project.Infrastructure.Data.Configurations
{
    public class FolderTagConfiguration : IEntityTypeConfiguration<FolderTag>
    {
        public void Configure(EntityTypeBuilder<FolderTag> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                FolderTagId => FolderTagId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => FolderTagId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.HasOne<Folder>()
                .WithMany()
                .HasForeignKey(o => o.FolderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Tag>()
                .WithMany()
                .HasForeignKey(o => o.TagId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
