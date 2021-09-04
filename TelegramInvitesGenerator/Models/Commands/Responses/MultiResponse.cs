using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class MultiResponse : IResponse
    {
        private readonly IResponse[] _responses;

        public MultiResponse(params IResponse[] responses)
        {
            _responses = responses;
        }
        
        public async Task SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            foreach (var answer in _responses)
            {
                await answer.SendAsync(botClient, chatId, cancellationToken);
            }
        }
    }
}