namespace Project.Application.Features.Storage.UpdateStoragePermission
{
    public class UpdateStoragePermissionRequest : ICommand<UpdateStoragePermissionResponse>
    {
        public int Id { get; set; }
        public bool IsFile { get; set; }
        public Access Access { get; set; }
        public Dictionary<int, Access> TargetPermission { get; set; }
    }
}
