using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Models.Commands.Answers;
using TelegramInvitesGenerator.Models.Commands.Questions;

namespace TelegramInvitesGenerator.Models.Commands
{
    public interface IBotCommand
    {
        IQuestion Question { get; }

        Task<IAnswer> GetAnswerAsync(BotMessage message);

        public static readonly IAnswer UnknownCommandAnswer = 
            new TextAnswer("Неизвестная команда. Для получения помощи отправьте /help");
    }
}