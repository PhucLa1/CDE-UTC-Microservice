using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects.Id
{
    public class FolderId
    {
        public Guid Value { get; set; }
        private FolderId(Guid value) => Value = value;
        public static FolderId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("FolderId cannot be empty");
            }
            return new FolderId(value);
        }
    }
}
