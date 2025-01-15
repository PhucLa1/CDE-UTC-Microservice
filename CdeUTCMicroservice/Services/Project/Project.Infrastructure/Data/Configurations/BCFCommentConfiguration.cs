

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

            builder.HasOne<BCFTopic>()
                .WithMany()
                .HasForeignKey(o => o.BCFTopicId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
