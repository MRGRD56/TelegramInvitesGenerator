using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class AsyncMultiResponse : IResponse
    {
        private readonly IAsyncEnumerable<IResponse> _responses;

        public AsyncMultiResponse(IAsyncEnumerable<IResponse> responses)
        {
            _responses = responses;
        }
        
        public async Task SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            await foreach (var answer in _responses.WithCancellation(cancellationToken))
            {
                await answer.SendAsync(botClient, chatId, cancellationToken);
            }
        }
    }
}