namespace Core.Application.Dtos.Responses
{
    public class TodoUseCaseResponse
    {
        public int Id { get; private set; }

        public string Title { get; private set; }

        public bool Done { get; private set; }

        public TodoUseCaseResponse(int id, string title, bool done)
        {
            Id = id;
            Title = title;
            Done = done;
        }
    }
}