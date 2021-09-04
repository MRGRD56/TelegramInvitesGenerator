using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Extensions;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class RemoveMessageResponse : IResponse
    {
        private readonly Message _message;

        public RemoveMessageResponse(Message message)
        {
            _message = message;
        }
        
        public Task SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            return TelegramApi.ExecuteAsync(botClient.DeleteMessageAsync(chatId, _message.MessageId, cancellationToken));
        }
    }
}