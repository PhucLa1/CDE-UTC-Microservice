
namespace Project.Infrastructure.Data.Configurations
{
    public class ViewBCFTopicConfiguration : IEntityTypeConfiguration<ViewBCFTopic>
    {
        public void Configure(EntityTypeBuilder<ViewBCFTopic> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                ViewBCFTopicId => ViewBCFTopicId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => ViewBCFTopicId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject
            builder.HasOne<View>()
              .WithMany()
              .HasForeignKey(o => o.ViewId)
              .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne<BCFTopic>()
              .WithMany()
              .HasForeignKey(o => o.BCFTopicId)
              .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
