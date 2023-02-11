namespace Core.Application.Dtos.Queries
{
    public class GetTodoQuery
    {
        public int Id { get; private set; }

        public string Title { get; private set; }

        public bool Done { get; private set; }

        public GetTodoQuery(int id, string title, bool done)
        {
            Id = id;
            Title = title;
            Done = done;
        }
    }
}