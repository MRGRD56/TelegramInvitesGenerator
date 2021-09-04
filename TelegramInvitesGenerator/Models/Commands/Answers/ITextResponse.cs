namespace TelegramInvitesGenerator.Models.Commands.Answers
{
    public interface ITextResponse : IResponse
    {
        string Text { get; }
    }
}