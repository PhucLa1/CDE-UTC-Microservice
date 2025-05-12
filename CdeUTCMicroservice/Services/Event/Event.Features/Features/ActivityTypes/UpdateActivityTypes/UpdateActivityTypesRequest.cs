namespace Event.Features.Features.ActivityTypes.UpdateActivityTypes
{
    public class UpdateActivityTypesRequest : ICommand<UpdateActivityTypesResponse>
    {
        public List<UpdateActivityTypesDto> UpdateActivityTypesDtos { get; set; }
        public bool IsAcceptAll { get; set; }
        public int ProjectId { get; set; }
    }

    public class UpdateActivityTypesDto
    {
        public int Id { get; set; }
        public TimeSpan TimeSend { get; set; }
        public bool Enabled { get; set; }
    }
}
