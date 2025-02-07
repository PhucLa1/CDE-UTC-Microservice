namespace Project.Infrastructure.Data.Configurations
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {           
            builder.HasOne<Group>()
                .WithMany()
                .HasForeignKey(o => o.GroupId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
