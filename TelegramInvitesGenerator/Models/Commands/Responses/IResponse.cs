using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Models.Commands.Responses.Results;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public interface IResponse
    {
        Task<ResponseResult> SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default);
        
        public static readonly IResponse UnknownCommandResponse = 
            new TextResponse("Неизвестная команда. Для получения помощи отправьте /help");
    }
}