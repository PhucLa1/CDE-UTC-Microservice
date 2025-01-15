using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class View : Aggregate<ViewId>
    {
        public FileId FileId { get; private set; } = default!;
        public ViewType ViewType { get; private set; }
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public static View Create(FileId fileId, ViewType viewType, string name, string description)
        {
            var view = new View
            {
                FileId = fileId,
                ViewType = viewType,
                Name = name,
                Description = description
            };
            return view;
        }
    }
}
