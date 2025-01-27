
namespace Project.Infrastructure.Data.Configurations
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                UserGroupId => UserGroupId.Value, // Chuyển từ ValueObject -> Giá trị trong table
                dbId => UserGroupId.Of(dbId));  // Chuyển từ Table SQL -> Giá trị ValueObject

            builder.HasOne<Group>()
                .WithMany()
                .HasForeignKey(o => o.GroupId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
