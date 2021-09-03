namespace TelegramInvitesGenerator.Models.Commands.Questions
{
    public interface IQuestion
    {
        bool IsMatch(string userQuestion);
    }
}