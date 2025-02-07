namespace Project.Infrastructure.Data.Configurations
{
    public class FolderTagConfiguration : IEntityTypeConfiguration<FolderTag>
    {
        public void Configure(EntityTypeBuilder<FolderTag> builder)
        {           
            builder.HasOne<Tag>()
                .WithMany()
                .HasForeignKey(o => o.TagId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
