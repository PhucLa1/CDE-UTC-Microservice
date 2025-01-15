using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects.Id
{
    public class FileBCFTopicId
    {
        public Guid Value { get; private set; }
        private FileBCFTopicId(Guid value) => Value = value;
        public static FileBCFTopicId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("FileBCFTopicId cannot be empty");
            }
            return new FileBCFTopicId(value);
        }
    }
}
