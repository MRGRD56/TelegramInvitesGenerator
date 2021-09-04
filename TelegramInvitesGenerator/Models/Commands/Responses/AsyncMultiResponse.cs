using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Models.Commands.Responses.Messages;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class AsyncMultiResponse : IResponse
    {
        private readonly IAsyncEnumerable<IResponse> _responses;

        public AsyncMultiResponse(IAsyncEnumerable<IResponse> responses)
        {
            _responses = responses;
        }
        
        public async Task<ResponseResult> SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            var messages = new List<Message>();
            await foreach (var answer in _responses.WithCancellation(cancellationToken))
            {
                var newMessages = await answer.SendAsync(botClient, chatId, cancellationToken);
                messages.AddRange(newMessages.Messages);
            }

            var result = new ResponseResult(messages);
            Sent?.Invoke(this, new ResponseSentEventArgs(result));
            return result;
        }

        public event EventHandler<ResponseSentEventArgs> Sent;
    }
}