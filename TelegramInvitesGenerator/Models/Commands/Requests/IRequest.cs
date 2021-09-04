namespace TelegramInvitesGenerator.Models.Commands.Requests
{
    public interface IRequest
    {
        bool IsMatch(string userQuestion);
    }
}