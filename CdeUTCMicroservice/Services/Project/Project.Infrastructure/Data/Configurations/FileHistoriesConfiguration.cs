namespace Project.Infrastructure.Data.Configurations
{
    public class FileHistoriesConfiguration : IEntityTypeConfiguration<FileHistory>
    {
        public void Configure(EntityTypeBuilder<FileHistory> builder)
        {
            builder.HasOne<File>()
                .WithMany()
                .HasForeignKey(o => o.FileId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
