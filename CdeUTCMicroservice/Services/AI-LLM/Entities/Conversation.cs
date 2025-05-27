namespace AI_LLM.Entities
{
    public class Conversation : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public int ProjectId { get; set; }
        public bool IsActive { get; set; }
        public string Context { get; set; } = string.Empty;
        public List<Message> Messages { get; set; }
    }
}
