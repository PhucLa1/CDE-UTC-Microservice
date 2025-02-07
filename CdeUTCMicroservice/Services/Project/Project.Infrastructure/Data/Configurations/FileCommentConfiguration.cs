namespace Project.Infrastructure.Data.Configurations
{
    public class FileCommentConfiguration : IEntityTypeConfiguration<FileComment>
    {
        public void Configure(EntityTypeBuilder<FileComment> builder)
        {
            builder.HasOne<File>()
              .WithMany()
              .HasForeignKey(o => o.FileId)
              .OnDelete(DeleteBehavior.SetNull);          
        }
    }
}
