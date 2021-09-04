using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramInvitesGenerator.Models.Commands.Answers
{
    public class TextResponse : ITextResponse
    {
        public string Text { get; }

        public TextResponse(string text)
        {
            Text = text;
        }
        
        public async Task SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            await botClient.SendTextMessageAsync(chatId, Text, cancellationToken: cancellationToken);
        }
    }
}