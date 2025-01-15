using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects.Id
{
    public class FolderPermissionId
    {
        public Guid Value { get; private set; }
        private FolderPermissionId(Guid value) => Value = value;
        public static FolderPermissionId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("FolderPermissionId cannot be empty");
            }
            return new FolderPermissionId(value);
        }
    }
}
