using BuildingBlocks.Enums;

namespace Event.Features.Features.ActivityTypes.GetActivityTypes
{
    public class GetActivityTypesReponse 
    {
        public int Id { get; set; }
        public TypeActivity TypeActivity { get; set; }
        public int ProjectId { get; set; }
        public bool IsAcceptAll { get; set; }
        public TimeSpan TimeSend { get; set; }
        public bool Enabled { get; set; }
    }
}
