using Core.Application.Resources;
using Lib.Notification.Abstractions;
using Lib.Notification.Extensions;

namespace Core.Application.Dtos.Requests
{
    public class UpdateTodoUseCaseRequest : Notifiable
    {
        public int Id { get; private set; }

        public string Title { get; private set; }

        public bool Done { get; private set; }

        public UpdateTodoUseCaseRequest(int id, string title, bool done)
        {
            Id = id;
            Title = title;
            Done = done;

            Validate();
        }

        private void Validate()
        {
            if (Id <= Decimal.Zero)
                AddErrorNotification(Msg.IDENTIFIER_X0_IS_INVALID_COD, Msg.IDENTIFIER_X0_IS_INVALID_TXT.ToFormat(Id));

            if (String.IsNullOrWhiteSpace(Title))
                AddErrorNotification(Msg.X0_IS_REQUIRED_COD, Msg.X0_IS_REQUIRED_TXT.ToFormat("Title"));
        }
    }
}