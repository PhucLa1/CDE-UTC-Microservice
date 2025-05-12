namespace Event.Features.Features.ActivityTypes.GetActivityTypes
{
    public class GetActivityTypesRequest : IQuery<ApiResponse<List<GetActivityTypesReponse>>>
    {
        public int ProjectId { get; set; }
    }
}
