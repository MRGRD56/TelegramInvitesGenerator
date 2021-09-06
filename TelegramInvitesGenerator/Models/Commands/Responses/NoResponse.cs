using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Models.Commands.Responses.Results;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class NoResponse : IResponse
    {
        public Task<ResponseResult> SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(ResponseResult.Empty);
        }
    }
}