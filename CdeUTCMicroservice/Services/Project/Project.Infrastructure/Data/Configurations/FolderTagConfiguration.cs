namespace Project.Infrastructure.Data.Configurations
{
    public class FolderTagConfiguration : IEntityTypeConfiguration<FolderTag>
    {
        public void Configure(EntityTypeBuilder<FolderTag> builder)
        {           
            builder.HasOne(e => e.Tag)
                .WithMany(e => e.FolderTags)
                .HasForeignKey(o => o.TagId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(e => e.Folder)
                .WithMany(e => e.FolderTags)
                .HasForeignKey(o => o.FolderId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
