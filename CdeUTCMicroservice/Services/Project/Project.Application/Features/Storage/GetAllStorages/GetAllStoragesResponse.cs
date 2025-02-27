namespace Project.Application.Features.Storage.GetAllStorages
{
    public class GetAllStoragesResponse
    {
        public int Id { get; set; }
        public bool IsFile { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public string NameCreatedBy { get; set; } = string.Empty;
        public string UrlImage { get; set; } = string.Empty;
        public List<string> TagNames { get; set; } = new List<string>() { };
    }
}
