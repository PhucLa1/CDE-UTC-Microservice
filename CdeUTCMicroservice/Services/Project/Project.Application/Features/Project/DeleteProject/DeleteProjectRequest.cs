namespace Project.Application.Features.Project.DeleteProject
{
    public class DeleteProjectRequest : ICommand<DeleteProjectResponse>
    {
        public int Id { get; set; }
    }
}
