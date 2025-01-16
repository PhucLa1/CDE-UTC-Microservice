namespace BuildingBlocks.Pagination
{
    public class PaginationRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortColumn { get; set; } // Tên cột sắp xếp
        public bool SortDescending { get; set; } = false; // Sắp xếp giảm dần
        public string? SearchTerm { get; set; } // Từ khóa tìm kiếm
        public Dictionary<string, string>? Filters { get; set; } // Các bộ lọc theo cột
    };
}
