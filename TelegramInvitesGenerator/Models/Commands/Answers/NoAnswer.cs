using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramInvitesGenerator.Models.Commands.Answers
{
    public class NoAnswer : IAnswer
    {
        public Task SendAsync(ITelegramBotClient botClient, ChatId chatId)
        {
            return Task.CompletedTask;
        }
    }
}