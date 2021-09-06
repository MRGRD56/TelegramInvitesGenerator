using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Extensions;
using TelegramInvitesGenerator.Models.Commands.Responses.Results;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class TextResponse : ITextResponse
    {
        public string Text { get; }

        public TextResponse(string text)
        {
            Text = text;
        }

        public async Task<ResponseResult> SendAsync(ITelegramBotClient botClient, ChatId chatId,
            CancellationToken cancellationToken = default)
        {
            var message = await TelegramApi.ExecuteAsync(botClient.SendTextMessageAsync(chatId, Text,
                cancellationToken: cancellationToken));
            return new ResponseResult(message);
        }
    }
}