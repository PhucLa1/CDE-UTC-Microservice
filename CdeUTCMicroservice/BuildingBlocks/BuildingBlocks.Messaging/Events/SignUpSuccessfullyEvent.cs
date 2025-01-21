namespace BuildingBlocks.Messaging.Events
{
    public class SignUpSuccessfullyEvent
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string MobilePhonenumber { get; set; } = default!;
    }
}
