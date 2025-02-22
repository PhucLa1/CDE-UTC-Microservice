
namespace Project.Infrastructure.Data.Configurations
{
    public class FolderHistoryConfiguration : IEntityTypeConfiguration<FolderHistory>
    {
        public void Configure(EntityTypeBuilder<FolderHistory> builder)
        {
            builder.HasOne(e => e.Folder)
                .WithMany(e => e.FolderHistories)
                .HasForeignKey(o => o.FolderId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
