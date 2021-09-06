using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Extensions;
using TelegramInvitesGenerator.Models.Commands.Responses.Results;

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
            await TelegramApi.ExecuteAsync(botClient.DeleteMessageAsync(chatId, _message.MessageId, cancellationToken));
            return ResponseResult.Empty;
        }
    }
}