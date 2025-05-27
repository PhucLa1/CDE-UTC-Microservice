namespace AI_LLM.Entities
{
    public class Message : BaseEntity
    {
        public int ConversationId { get; set; }
        public int ProjectId { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsAI { get; set; }
        public string Context { get; set; } = string.Empty;
        public Conversation Conversation { get; set; }
    }
}
