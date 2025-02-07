namespace Project.Infrastructure.Data.Configurations
{
    public class ViewBCFTopicConfiguration : IEntityTypeConfiguration<ViewBCFTopic>
    {
        public void Configure(EntityTypeBuilder<ViewBCFTopic> builder)
        {
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
