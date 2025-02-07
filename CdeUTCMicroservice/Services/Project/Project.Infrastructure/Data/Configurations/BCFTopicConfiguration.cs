namespace Project.Infrastructure.Data.Configurations
{
    public class BCFTopicConfiguration : IEntityTypeConfiguration<BCFTopic>
    {
        public void Configure(EntityTypeBuilder<BCFTopic> builder)
        {
            builder.HasOne(x => x.Project)
                .WithMany(x => x.BCFTopics)
                .HasForeignKey(o => o.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.OwnsOne(o => o.Characteristic, characteristicBuilder =>
            {
                characteristicBuilder.HasOne(n => n.Type)
                    .WithMany()
                    .HasForeignKey(n => n.TypeId)
                    .OnDelete(DeleteBehavior.SetNull);

                characteristicBuilder.HasOne(n => n.Status)
                    .WithMany()
                    .HasForeignKey(n => n.StatusId)
                    .OnDelete(DeleteBehavior.SetNull);

                characteristicBuilder.HasOne(n => n.Priority)
                    .WithMany()
                    .HasForeignKey(n => n.PriorityId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
