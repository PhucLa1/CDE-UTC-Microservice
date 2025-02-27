using Project.Application.Dtos.Models;

namespace Project.Application.Features.Views.CreateView
{
    public class CreateViewRequest : ICommand<CreateViewResponse>
    {
        public int FileId { get; set; }
        public List<AnnotationModel> AnnotationModels { get; set; } = new List<AnnotationModel>();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
