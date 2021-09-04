using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramInvitesGenerator.Models.Commands.Answers
{
    public class MultiResponse : IResponse
    {
        private readonly IResponse[] _answers;

        public MultiResponse(params IResponse[] answers)
        {
            _answers = answers;
        }
        
        public async Task SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            foreach (var answer in _answers)
            {
                await answer.SendAsync(botClient, chatId, cancellationToken);
            }
        }
    }
}