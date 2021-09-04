using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Models.Commands.Responses.Messages;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class RemoveMessageResponse : IResponse
    {
        private readonly Message _message;

        public RemoveMessageResponse(Message message)
        {
            _message = message;
        }
        
        public async Task<ResponseResult> SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            await botClient.DeleteMessageAsync(chatId, _message.MessageId, cancellationToken);
            var result = ResponseResult.Empty;
            Sent?.Invoke(this, new ResponseSentEventArgs(result));
            return result;
        }

        public event EventHandler<ResponseSentEventArgs> Sent;
    }
}