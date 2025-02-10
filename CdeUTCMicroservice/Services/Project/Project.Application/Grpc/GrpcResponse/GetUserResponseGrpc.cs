namespace Project.Application.Grpc.GrpcResponse
{
    public class GetUserResponseGrpc
    {
        public string FullName { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
