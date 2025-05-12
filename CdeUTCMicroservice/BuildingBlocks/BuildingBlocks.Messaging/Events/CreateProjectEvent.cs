namespace BuildingBlocks.Messaging.Events
{
    public class CreateProjectEvent : IntergrationEvent
    {
        public int ProjectId { get; set; }
    }
}
