namespace Project.Application.Dtos.Result
{
    public class StoragePermissionResult
    {
        public int Id { get; set; }
        public int TargetId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Access Access { get; set; }
        public string Url { get; set; }
    }
}
