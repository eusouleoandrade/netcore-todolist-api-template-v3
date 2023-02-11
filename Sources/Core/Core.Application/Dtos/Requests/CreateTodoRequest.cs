namespace Core.Application.Dtos.Requests
{
    public class CreateTodoRequest
    {
        public string Title { get; private set; }

        public CreateTodoRequest(string title)
        {
            Title = title;
        }
    }
}