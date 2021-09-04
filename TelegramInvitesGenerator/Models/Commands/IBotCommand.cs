using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Models.Commands.Answers;
using TelegramInvitesGenerator.Models.Commands.Questions;

namespace TelegramInvitesGenerator.Models.Commands
{
    public interface IBotCommand
    {
        IRequest Request { get; }

        Task<IResponse> GetResponseAsync(BotMessage message);
    }
}