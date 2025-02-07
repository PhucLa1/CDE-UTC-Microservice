namespace Project.Infrastructure.Data.Configurations
{
    public class FolderCommentConfiguration : IEntityTypeConfiguration<FolderComment>
    {
        public void Configure(EntityTypeBuilder<FolderComment> builder)
        {
            builder.HasOne<Folder>()
                .WithMany()
                .HasForeignKey(o => o.FolderId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
