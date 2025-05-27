public class GeminiAskRequest
{
    public string Question { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public int? ConversationId { get; set; }
}