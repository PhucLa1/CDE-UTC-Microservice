namespace Project.Infrastructure.Data.Configurations
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {           
            builder.HasOne(x => x.Group)
                .WithMany(x => x.UserGroups)
                .HasForeignKey(o => o.GroupId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
