namespace Project.Infrastructure.Data.Configurations
{
    public class ViewCommentConfiguration : IEntityTypeConfiguration<ViewComment>
    {
        public void Configure(EntityTypeBuilder<ViewComment> builder)
        {
            builder.HasOne<View>()
              .WithMany()
              .HasForeignKey(o => o.ViewId)
              .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
