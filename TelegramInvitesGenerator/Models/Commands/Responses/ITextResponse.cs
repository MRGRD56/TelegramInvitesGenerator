namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public interface ITextResponse : IResponse
    {
        string Text { get; }
    }
}