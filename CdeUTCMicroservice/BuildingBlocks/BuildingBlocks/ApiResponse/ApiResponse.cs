namespace BuildingBlocks.ApiResponse
{
    public class ApiResponse<T>
    {
        public T Data { get; set; } = default!;
        public string Message { get; set; } = default!;
    }
}
