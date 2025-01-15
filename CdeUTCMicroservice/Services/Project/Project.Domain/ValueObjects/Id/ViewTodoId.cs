

using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects
{
    public class ViewTodoId
    {
        public Guid Value { get; private set; }
        private ViewTodoId(Guid value) => Value = value;
        public static ViewTodoId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("ViewTodoId cannot be empty");
            }
            return new ViewTodoId(value);
        }
    }
}
