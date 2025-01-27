

namespace Project.Infrastructure.Data.Configurations
{
    public class BCFCommentConfiguration : IEntityTypeConfiguration<BCFComment>
    {
        public void Configure(EntityTypeBuilder<BCFComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                BCFCommentId => BCFCommentId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => BCFCommentId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.HasOne(x => x.BCFTopic)
                .WithMany(x => x.BCFComments)
                .HasForeignKey(o => o.BCFTopicId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
