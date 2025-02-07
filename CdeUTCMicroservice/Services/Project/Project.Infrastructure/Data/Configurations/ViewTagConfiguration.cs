namespace Project.Infrastructure.Data.Configurations
{
    public class ViewTagConfiguration : IEntityTypeConfiguration<ViewTag>
    {
        public void Configure(EntityTypeBuilder<ViewTag> builder)
        {          
            builder.HasOne<View>()
              .WithMany()
              .HasForeignKey(o => o.ViewId)
              .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne<Tag>()
              .WithMany()
              .HasForeignKey(o => o.TagId)
              .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
