namespace TelegramInvitesGenerator.Models.Commands.Questions
{
    public interface IRequest
    {
        bool IsMatch(string userQuestion);
    }
}