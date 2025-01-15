
using Project.Domain.ValueObjects.Id;

namespace Project.Infrastructure.Data.Configurations
{
    public class FolderCommentConfiguration : IEntityTypeConfiguration<FolderComment>
    {
        public void Configure(EntityTypeBuilder<FolderComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                FolderCommentId => FolderCommentId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => FolderCommentId.Of(dbId));

            builder.HasOne<Folder>()
                .WithMany()
                .HasForeignKey(o => o.FolderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
