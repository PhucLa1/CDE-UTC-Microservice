namespace Project.Application.Features.Views.UpdateView
{
    public class UpdateViewRequest : ICommand<UpdateViewResponse>
    {
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int? ProjectId { get; set; }
        public List<int> TagIds { get; set; } = new List<int> { };
    }
}
