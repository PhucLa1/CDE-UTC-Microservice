namespace Project.Infrastructure.Data.Configurations
{
    public class FileTagConfiguration : IEntityTypeConfiguration<FileTag>
    {
        public void Configure(EntityTypeBuilder<FileTag> builder)
        {          
            builder.HasOne(e => e.File)
                .WithMany(e => e.FileTags)
                .HasForeignKey(o => o.FileId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(e => e.Tag)
                .WithMany(e => e.FileTags)
                .HasForeignKey(o => o.TagId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
