using Core.Application.Resources;
using Lib.Notification.Abstractions;
using Lib.Notification.Extensions;

namespace Core.Application.Dtos.Requests
{
    public class CreateTodoUseCaseRequest : Notifiable
    {
        public string Title { get; private set; }

        public bool Done { get; private set; } = false;

        public CreateTodoUseCaseRequest(string title)
        {
            Title = title;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Title))
                AddErrorNotification(Msg.X0_IS_REQUIRED_COD, Msg.X0_IS_REQUIRED_TXT.ToFormat("Title"));
        }
    }
}
