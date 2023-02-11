namespace Core.Application.Dtos.Queries
{
    public class TodoQuery
    {
        public int Id { get; private set; }

        public string Title { get; private set; }

        public bool Done { get; private set; }

        public TodoQuery(int id, string title, bool done)
        {
            Id = id;
            Title = title;
            Done = done;
        }
    }
}