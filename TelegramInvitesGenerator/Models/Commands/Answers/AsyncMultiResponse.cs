using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramInvitesGenerator.Models.Commands.Answers
{
    public class AsyncMultiResponse : IResponse
    {
        private readonly IAsyncEnumerable<IResponse> _answers;

        public AsyncMultiResponse(IAsyncEnumerable<IResponse> answers)
        {
            _answers = answers;
        }
        
        public async Task SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            await foreach (var answer in _answers.WithCancellation(cancellationToken))
            {
                await answer.SendAsync(botClient, chatId, cancellationToken);
            }
        }
    }
}