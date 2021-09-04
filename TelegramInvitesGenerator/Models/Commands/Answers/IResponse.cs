using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramInvitesGenerator.Models.Commands.Answers
{
    public interface IResponse
    {
        Task SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default);
        
        public static readonly IResponse UnknownCommandResponse = 
            new TextResponse("Неизвестная команда. Для получения помощи отправьте /help");
    }
}