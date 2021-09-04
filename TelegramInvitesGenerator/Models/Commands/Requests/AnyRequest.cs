namespace TelegramInvitesGenerator.Models.Commands.Requests
{
    public class AnyRequest : IRequest
    {
        public bool IsMatch(string userQuestion) => true;
    }
}