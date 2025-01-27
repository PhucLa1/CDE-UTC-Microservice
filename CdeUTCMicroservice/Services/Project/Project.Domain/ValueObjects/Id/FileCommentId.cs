using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects.Id
{
    public class FileCommentId
    {
        public Guid Value { get; set; }
        private FileCommentId(Guid value) => Value = value;
        public static FileCommentId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("FileCommentId cannot be empty");
            }
            return new FileCommentId(value);
        }
    }
}
