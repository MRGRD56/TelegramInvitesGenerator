using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class NoResponse : IResponse
    {
        public Task SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}