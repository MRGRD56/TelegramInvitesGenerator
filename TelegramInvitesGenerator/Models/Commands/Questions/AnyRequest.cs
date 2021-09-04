namespace TelegramInvitesGenerator.Models.Commands.Questions
{
    public class AnyRequest : IRequest
    {
        public bool IsMatch(string userQuestion) => true;
    }
}