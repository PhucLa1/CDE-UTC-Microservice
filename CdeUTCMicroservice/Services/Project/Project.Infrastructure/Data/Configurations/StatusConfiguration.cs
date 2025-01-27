
namespace Project.Infrastructure.Data.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                StatusId => StatusId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => StatusId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.HasOne(x => x.Project)
                .WithMany(x => x.Statuses)
                .HasForeignKey(o => o.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
