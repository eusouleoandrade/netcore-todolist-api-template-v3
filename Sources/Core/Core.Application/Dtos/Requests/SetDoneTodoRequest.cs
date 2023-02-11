namespace Core.Application.Dtos.Requests
{
    public class SetDoneTodoRequest
    {
        public bool Done { get; private set; }

        public SetDoneTodoRequest(bool done)
        {
            Done = done;
        }
    }
}