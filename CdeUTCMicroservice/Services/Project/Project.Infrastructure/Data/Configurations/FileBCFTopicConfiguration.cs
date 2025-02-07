namespace Project.Infrastructure.Data.Configurations
{
    public class FileBCFTopicConfiguration : IEntityTypeConfiguration<FileBCFTopic>
    {
        public void Configure(EntityTypeBuilder<FileBCFTopic> builder)
        {        
            builder.HasOne<File>()
               .WithMany()
               .HasForeignKey(o => o.FileId)
               .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne<BCFTopic>()
               .WithMany()
               .HasForeignKey(o => o.BCFTopicId)
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
