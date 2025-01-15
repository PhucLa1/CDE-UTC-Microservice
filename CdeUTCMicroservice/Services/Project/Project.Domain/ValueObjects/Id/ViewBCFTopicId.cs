using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects.Id
{
    public class ViewBCFTopicId
    {
        public Guid Value { get; private set; }
        private ViewBCFTopicId(Guid value) => Value = value;
        public static ViewBCFTopicId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("ViewBCFTopicId cannot be empty");
            }
            return new ViewBCFTopicId(value);
        }
    }
}
