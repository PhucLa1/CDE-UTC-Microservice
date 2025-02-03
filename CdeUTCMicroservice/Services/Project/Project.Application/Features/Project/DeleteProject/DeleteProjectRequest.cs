namespace Project.Application.Features.Project.DeleteProject
{
    public class DeleteProjectRequest : ICommand<DeleteProjectResponse>
    {
        public Guid Id { get; set; }
    }
}
