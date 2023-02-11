namespace Core.Application.Dtos.Requests
{
    public class UpdateTodoRequest
    {
        public string Title { get; private set; }

        public bool Done { get; private set; }

        public UpdateTodoRequest(string title, bool done)
        {
            Title = title;
            Done = done;
        }
    }
}