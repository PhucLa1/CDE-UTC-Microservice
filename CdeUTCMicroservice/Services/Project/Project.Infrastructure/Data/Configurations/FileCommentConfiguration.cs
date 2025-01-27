
namespace Project.Infrastructure.Data.Configurations
{
    public class FileCommentConfiguration : IEntityTypeConfiguration<FileComment>
    {
        public void Configure(EntityTypeBuilder<FileComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                FileCommentId => FileCommentId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => FileCommentId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject
            
            builder.HasOne<File>()
              .WithMany()
              .HasForeignKey(o => o.FileId)
              .OnDelete(DeleteBehavior.SetNull); 

        }
    }
}
