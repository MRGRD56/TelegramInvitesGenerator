using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramInvitesGenerator.Models.Commands.Answers
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
            return botClient.DeleteMessageAsync(chatId, _message.MessageId, cancellationToken);
        }
    }
}