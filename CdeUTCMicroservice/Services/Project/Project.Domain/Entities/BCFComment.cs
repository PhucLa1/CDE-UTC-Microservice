namespace Project.Domain.Entities
{
    public class BCFComment : Entity<BCFCommentId>
    {
        public string Content { get; private set; } = default!;
        public BCFTopicId BCFTopicId { get; private set; } = default!;

        public static BCFComment Create(string content, BCFTopicId bcfTopicId)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(content);
            var bcfComment = new BCFComment
            {
                Content = content,
                BCFTopicId = bcfTopicId,
            };
            return bcfComment;
        }
    }
}
