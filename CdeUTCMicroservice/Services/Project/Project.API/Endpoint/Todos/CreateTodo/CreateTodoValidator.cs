using FluentValidation;
using Project.Application.Features.Todos.CreateTodo;

namespace Project.API.Endpoint.Todos.CreateTodo
{
    public class CreateTodoValidator : AbstractValidator<CreateTodoRequest>
    {
        public CreateTodoValidator() 
        {
            
        }
    }
}
