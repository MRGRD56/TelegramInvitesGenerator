using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Extensions;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class AsyncMultiResponse : IResponse
    {
        private readonly IObservable<IResponse> _responses;

        public AsyncMultiResponse(IObservable<IResponse> responses)
        {
            _responses = responses;
        }
        
        public async Task SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            await _responses.ForEachAsync(async response =>
            {
                await response.SendAsync(botClient, chatId, cancellationToken);
            }, cancellationToken);
        }
    }
}