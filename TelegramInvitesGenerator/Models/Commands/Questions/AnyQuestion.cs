namespace TelegramInvitesGenerator.Models.Commands.Questions
{
    public class AnyQuestion : IQuestion
    {
        public bool IsMatch(string userQuestion) => true;
    }
}