
namespace Project.Infrastructure.Data.Configurations
{
    public class FileBCFTopicConfiguration : IEntityTypeConfiguration<FileBCFTopic>
    {
        public void Configure(EntityTypeBuilder<FileBCFTopic> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                FileBCFTopicId => FileBCFTopicId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => FileBCFTopicId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject
            
            builder.HasOne<File>()
               .WithMany()
               .HasForeignKey(o => o.FileId)
               .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne<BCFTopic>()
               .WithMany()
               .HasForeignKey(o => o.BCFTopicId)
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
