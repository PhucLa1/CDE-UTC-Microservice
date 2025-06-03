using Project.Application.Dtos.Models;

namespace Project.Application.Features.Storage.DeleteStorages
{
    public class DeleteStoragesRequest : ICommand<DeleteStoragesResponse>
    {
        public List<StorageModel>? StorageModels { get; set; }
    }

}
