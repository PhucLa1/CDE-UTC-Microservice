namespace Project.Infrastructure.Data.Configurations
{
    public class FileTagConfiguration : IEntityTypeConfiguration<FileTag>
    {
        public void Configure(EntityTypeBuilder<FileTag> builder)
        {          
            builder.HasOne<File>()
                .WithMany()
                .HasForeignKey(o => o.FileId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne<Tag>()
                .WithMany()
                .HasForeignKey(o => o.TagId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
