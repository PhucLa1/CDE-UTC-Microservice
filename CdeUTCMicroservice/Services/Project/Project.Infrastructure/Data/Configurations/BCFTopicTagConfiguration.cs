
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

            builder.HasOne<BCFTopic>()
                .WithMany()
                .HasForeignKey(o => o.BCFTopicId)
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
