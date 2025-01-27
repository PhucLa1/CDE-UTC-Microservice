using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects
{
    public class ProjectId
    {
        public Guid Value { get; set; }
        private ProjectId(Guid value) => Value = value;
        public static ProjectId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("ProjectId cannot be empty");
            }
            return new ProjectId(value);
        }
    }
}
