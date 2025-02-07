namespace Project.Infrastructure.Data.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {            
            builder.HasOne<Projects>()
                .WithMany()
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
