namespace Core.Application.Dtos.Responses
{
    public class CreateTodoUseCaseResponse
    {
        public int Id { get; private set; }

        public string Title { get; private set; }

        public bool Done { get; private set; }

        public CreateTodoUseCaseResponse(int id, string title, bool done)
        {
            Id = id;
            Title = title;
            Done = done;
        }
    }
}