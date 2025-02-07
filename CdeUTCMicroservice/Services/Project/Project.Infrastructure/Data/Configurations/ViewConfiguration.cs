namespace Project.Infrastructure.Data.Configurations
{
    public class ViewConfiguration : IEntityTypeConfiguration<View>
    {
        public void Configure(EntityTypeBuilder<View> builder)
        {
            builder.HasOne<File>()
              .WithMany()
              .HasForeignKey(o => o.FileId)
              .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
