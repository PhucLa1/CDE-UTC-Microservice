using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;

namespace Project.Application.Data
{
    public interface IProjectDBContext
    {

        DbSet<BCFComment> BCFComments { get; }
        DbSet<BCFTopic> BCFTopics { get; }
        DbSet<BCFTopicTag> BCFTopicTags { get; }
        DbSet<File> Files { get; }
        DbSet<FileBCFTopic> FileBCFTopics { get; }
        DbSet<FileComment> FileComments { get; }
        DbSet<FilePermission> FilePermissions { get; }
        DbSet<FileTag> FileTags { get; }
        DbSet<FileTodo> FileTodos { get; }
        DbSet<Folder> Folders { get; }
        DbSet<FolderComment> FolderComments { get; }
        DbSet<FolderPermission> FolderPermissions { get; }
        DbSet<FolderTag> FolderTags { get; }
        DbSet<Group> Groups { get; }
        DbSet<Priority> Priorities { get; }
        DbSet<Projects> Projects { get; }
        DbSet<Status> Statuses { get; }
        DbSet<Tag> Tags { get; }
        DbSet<Todo> Todos { get; }
        DbSet<TodoComment> TodoComments { get; }
        DbSet<TodoTag> TodoTags { get; }
        DbSet<Type> Types { get; }
        DbSet<UserGroup> UserGroups { get; }
        DbSet<UserProject> UserProjects { get; }
        DbSet<View> Views { get; }
        DbSet<ViewBCFTopic> ViewBCFTopics { get; }
        DbSet<ViewComment> ViewComments { get; }
        DbSet<ViewTag> ViewTags { get; }
        DbSet<ViewTodo> ViewTodos { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
