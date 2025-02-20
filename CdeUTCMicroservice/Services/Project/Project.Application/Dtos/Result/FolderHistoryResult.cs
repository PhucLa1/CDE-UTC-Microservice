namespace Project.Application.Dtos.Result
{
    public class FolderHistoryResult
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Version { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public string NameCreatedBy { get; set; } = string.Empty;
    }
}
