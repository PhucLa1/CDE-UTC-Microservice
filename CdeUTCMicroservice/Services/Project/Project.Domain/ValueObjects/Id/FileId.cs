using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects.Id
{
    public class FileId
    {
        public Guid Value { get; }

        private FileId(Guid value) => Value = value;
        public static FileId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("FileId cannot be empty");
            }
            return new FileId(value);
        }
    }
}
