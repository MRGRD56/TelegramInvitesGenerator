using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Models.Commands.Responses;
using TelegramInvitesGenerator.Models.Commands.Requests;

namespace TelegramInvitesGenerator.Models.Commands
{
    public interface IBotCommand
    {
        IRequest Request { get; }

        Task<IResponse> GetResponseAsync(BotMessage message);
    }
}