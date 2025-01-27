using System.Reflection;

namespace Project.Infrastructure.Data
{
    public class ProjectDBContext : DbContext
    {

        public ProjectDBContext(DbContextOptions<ProjectDBContext> options)
            : base(options) { }

        public DbSet<Projects> Projects => Set<Projects>();
        public DbSet<Folder> Folders => Set<Folder>();
        public DbSet<File> Files => Set<File>();
        public DbSet<BCFTopic> BCFTopics => Set<BCFTopic>();
        public DbSet<Priority> Priorities => Set<Priority>();
        public DbSet<Status> Statuses => Set<Status>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Todo> Todos => Set<Todo>();
        public DbSet<BCFComment> BCFComments => Set<BCFComment>();
        public DbSet<BCFTopicTag> BCFTopicTags => Set<BCFTopicTag>();
        public DbSet<FileBCFTopic> FileBCFTopics => Set<FileBCFTopic>();
        public DbSet<FileComment> FileComments => Set<FileComment>();
        public DbSet<FilePermission> FilePermissions => Set<FilePermission>();
        public DbSet<FileTag> FileTags => Set<FileTag>();
        public DbSet<FileTodo> FileTodos => Set<FileTodo>();
        public DbSet<FolderComment> FolderComments => Set<FolderComment>();
        public DbSet<FolderPermission> FolderPermissions => Set<FolderPermission>();
        public DbSet<FolderTag> FolderTags => Set<FolderTag>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<TodoComment> TodoComments => Set<TodoComment>();
        public DbSet<TodoTag> TodoTags => Set<TodoTag>();
        public DbSet<Domain.Entities.Type> Types => Set<Domain.Entities.Type>();
        public DbSet<UserGroup> UserGroups => Set<UserGroup>();
        public DbSet<UserProject> UserProjects => Set<UserProject>();
        public DbSet<View> Views => Set<View>();
        public DbSet<ViewBCFTopic> ViewBCFTopics => Set<ViewBCFTopic>();
        public DbSet<ViewComment> ViewComments => Set<ViewComment>();
        public DbSet<ViewTag> ViewTags => Set<ViewTag>();
        public DbSet<ViewTodo> ViewTodos => Set<ViewTodo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}