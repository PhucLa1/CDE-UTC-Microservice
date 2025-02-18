using Microsoft.AspNetCore.Http;
using Project.Domain.Abstractions;
using System.Reflection;

namespace Project.Infrastructure.Data
{
    public class ProjectDBContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProjectDBContext(DbContextOptions<ProjectDBContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public ProjectDBContext(DbContextOptions<ProjectDBContext> options)
            : base(options) { }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var userId = GetCurrentUserId();

            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added))
            {
                if (entry.Entity is IAuditable)
                {
                    var entity = (IAuditable)entry.Entity;
                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.UpdatedBy = userId;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = DateTime.UtcNow;
                        entity.CreatedBy = userId;
                    }
                }
            }
        }

        #region Get token from gateways

        public int GetCurrentUserId()
        {
            var context = _httpContextAccessor.HttpContext;
            var userIdObj = context.Request.Headers["X-UserId"].FirstOrDefault();
            if (userIdObj is null)
                return 0; ;
            if (int.TryParse(userIdObj.ToString(), out var userId))
            {
                return userId; // Trả về int nếu chuyển đổi thành công
            }
            return 0; // Trả về int mặc định nếu chuyển đổi thất bại
        }

        #endregion

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
        public DbSet<FileHistory> FileHistories => Set<FileHistory>();
        public DbSet<FileTag> FileTags => Set<FileTag>();
        public DbSet<FileTodo> FileTodos => Set<FileTodo>();
        public DbSet<FolderComment> FolderComments => Set<FolderComment>();
        public DbSet<FolderPermission> FolderPermissions => Set<FolderPermission>();
        public DbSet<FolderTag> FolderTags => Set<FolderTag>();
        public DbSet<FolderHistory> FolderHistorys => Set<FolderHistory>();
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