using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramInvitesGenerator.Models.Commands.Answers
{
    public interface IAnswer
    {
        Task SendAsync(ITelegramBotClient botClient, ChatId chatId);
    }
}