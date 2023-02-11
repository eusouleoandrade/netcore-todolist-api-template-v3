using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Core.Application.Resources;
using Lib.Notification.Models;

namespace Core.Application.Dtos.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class Response<TData>
        where TData : class

    {
        public bool Succeeded { get; set; }

        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IEnumerable<NotificationMessage>? Errors { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TData? Data { get; set; }

        public Response(TData data, bool succeeded, string? message = null, IEnumerable<NotificationMessage>? errors = null)
        {
            Succeeded = succeeded;

            Errors = errors;

            Data = data;

            if (string.IsNullOrWhiteSpace(message))
                Message = succeeded ? Msg.RESPONSE_SUCCEEDED_MESSAGE_TXT : Msg.RESPONSE_FAILED_MESSAGE_TXT;
            else
                Message = message;
        }
    }

    [ExcludeFromCodeCoverage]
    public class Response<TData, TErrors>
        where TData : class
        where TErrors : class

    {
        public bool Succeeded { get; set; }

        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TErrors? Errors { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TData? Data { get; set; }

        public Response(TData data, bool succeeded, string? message = null, TErrors? errors = null)
        {
            Succeeded = succeeded;

            Errors = errors;

            Data = data;

            if (string.IsNullOrWhiteSpace(message))
                Message = succeeded ? Msg.RESPONSE_SUCCEEDED_MESSAGE_TXT : Msg.RESPONSE_FAILED_MESSAGE_TXT;
            else
                Message = message;
        }
    }

    [ExcludeFromCodeCoverage]
    public class Response
    {
        public bool Succeeded { get; set; }

        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IEnumerable<NotificationMessage>? Errors { get; set; }

        public Response(bool succeeded, string? message = null, IEnumerable<NotificationMessage>? errors = null)
        {
            Succeeded = succeeded;

            Errors = errors;

            if (string.IsNullOrWhiteSpace(message))
                Message = succeeded ? Msg.RESPONSE_SUCCEEDED_MESSAGE_TXT : Msg.RESPONSE_FAILED_MESSAGE_TXT;
            else
                Message = message;
        }
    }
}