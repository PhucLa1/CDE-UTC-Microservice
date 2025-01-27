
namespace Project.Infrastructure.Data.Configurations
{
    public class BCFTopicTagConfiguration : IEntityTypeConfiguration<BCFTopicTag>
    {
        public void Configure(EntityTypeBuilder<BCFTopicTag> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                BCFTopicTagId => BCFTopicTagId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => BCFTopicTagId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.HasOne(x => x.BCFTopic)
                .WithMany(x => x.BCFTopicTags)
                .HasForeignKey(o => o.BCFTopicId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Tag)
                .WithMany(x => x.BCFTopicTags)
                .HasForeignKey(o => o.TagId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
