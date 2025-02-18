namespace Project.Application.Features.Project.GetProjectById
{
    public class GetProjectByIdResponse
    {
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Ownership { get; set; } = string.Empty;
        public int UserCount { get; set; }
        public int FolderCount { get; set; }
        public int FileCount { get; set; }
        public double Size { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
    }
}
