using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects.Id
{
    public class FilePermissionId
    {
        public Guid Value { get; private set; }
        private FilePermissionId(Guid value) => Value = value;
        public static FilePermissionId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("FilePermissionId cannot be empty");
            }
            return new FilePermissionId(value);
        }
    }
}
