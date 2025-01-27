
namespace Project.Infrastructure.Data.Configurations
{
    public class ViewTagConfiguration : IEntityTypeConfiguration<ViewTag>
    {
        public void Configure(EntityTypeBuilder<ViewTag> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                ViewTagId => ViewTagId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => ViewTagId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

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
