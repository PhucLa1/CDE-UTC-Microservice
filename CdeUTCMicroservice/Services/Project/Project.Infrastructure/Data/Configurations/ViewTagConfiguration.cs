namespace Project.Infrastructure.Data.Configurations
{
    public class ViewTagConfiguration : IEntityTypeConfiguration<ViewTag>
    {
        public void Configure(EntityTypeBuilder<ViewTag> builder)
        {          
            builder.HasOne(e => e.View)
              .WithMany(e => e.ViewTags)
              .HasForeignKey(o => o.ViewId)
              .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(e => e.Tag)
              .WithMany(e => e.ViewTags)
              .HasForeignKey(o => o.TagId)
              .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
