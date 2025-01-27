
namespace Project.Infrastructure.Data.Configurations
{
    public class PriorityConfiguration : IEntityTypeConfiguration<Priority>
    {
        public void Configure(EntityTypeBuilder<Priority> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                PriorityId => PriorityId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => PriorityId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.HasOne(x => x.Project)
                .WithMany(x => x.Priorities)
                .HasForeignKey(o => o.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
