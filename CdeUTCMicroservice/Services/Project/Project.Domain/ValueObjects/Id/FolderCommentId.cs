using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects.Id
{
    public class FolderCommentId
    {
        public Guid Value { get; private set; }
        private FolderCommentId(Guid value) => Value = value;
        public static FolderCommentId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("FolderCommentId cannot be empty");
            }
            return new FolderCommentId(value);
        }
    }
}
