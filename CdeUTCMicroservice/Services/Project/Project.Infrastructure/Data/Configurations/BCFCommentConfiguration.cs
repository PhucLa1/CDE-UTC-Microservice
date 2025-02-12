namespace Project.Infrastructure.Data.Configurations
{
    public class BCFCommentConfiguration : IEntityTypeConfiguration<BCFComment>
    {
        public void Configure(EntityTypeBuilder<BCFComment> builder)
        {
            builder.HasOne(x => x.BCFTopic)
                .WithMany(x => x.BCFComments)
                .HasForeignKey(o => o.BCFTopicId)
                .OnDelete(DeleteBehavior.SetNull);           
        }
    }
}
