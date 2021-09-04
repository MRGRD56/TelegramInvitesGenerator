using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Extensions;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class TextResponse : ITextResponse
    {
        public string Text { get; }

        public TextResponse(string text)
        {
            Text = text;
        }
        
        public Task SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            return TelegramApi.ExecuteAsync(botClient.SendTextMessageAsync(chatId, Text, cancellationToken: cancellationToken));
        }
    }
}