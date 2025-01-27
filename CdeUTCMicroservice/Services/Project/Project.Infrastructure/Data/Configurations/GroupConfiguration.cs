
namespace Project.Infrastructure.Data.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                GroupId => GroupId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => GroupId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.HasOne<Projects>()
                .WithMany()
                .HasForeignKey(o => o.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
