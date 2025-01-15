using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects.Id
{
    public class FolderTagId
    {
        public Guid Value { get; private set; }
        private FolderTagId(Guid value) => Value = value;
        public static FolderTagId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("FolderTagId cannot be empty");
            }
            return new FolderTagId(value);
        }
    }
}
