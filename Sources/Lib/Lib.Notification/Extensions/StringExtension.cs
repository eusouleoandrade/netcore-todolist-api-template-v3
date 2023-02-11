namespace Lib.Notification.Extensions
{
    public static class StringExtension
    {
        public static string ToFormat(this string mensagem, params object[] parametros)
        {
            return string.Format(mensagem, parametros);
        }
    }
}