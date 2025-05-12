namespace Event.Infrastructure.Grpc.GrpcRequest
{
    public class GetUserRequestGrpc
    {
        public List<int> Ids { get; set; } = new List<int>();
    }
}
