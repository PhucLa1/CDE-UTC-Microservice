
namespace Project.Infrastructure.Data.Configurations
{
    public class ViewCommentConfiguration : IEntityTypeConfiguration<ViewComment>
    {
        public void Configure(EntityTypeBuilder<ViewComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                ViewCommentId => ViewCommentId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => ViewCommentId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.HasOne<View>()
              .WithMany()
              .HasForeignKey(o => o.ViewId)
              .IsRequired()
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
