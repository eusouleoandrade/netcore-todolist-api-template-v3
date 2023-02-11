namespace Core.Application.Dtos.Requests
{
    public class SetDoneTodoUseCaseRequest
    {
        public int Id { get; private set; }

        public bool Done { get; private set; }

        public SetDoneTodoUseCaseRequest(int id, bool done)
        {
            Id = id;
            Done = done;
        }
    }
}