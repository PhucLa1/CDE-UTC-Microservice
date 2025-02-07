namespace Project.Infrastructure.Data.Configurations
{
    public class BCFTopicTagConfiguration : IEntityTypeConfiguration<BCFTopicTag>
    {
        public void Configure(EntityTypeBuilder<BCFTopicTag> builder)
        {



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
