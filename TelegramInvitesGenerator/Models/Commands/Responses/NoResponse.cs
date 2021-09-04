using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Models.Commands.Responses.Messages;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class NoResponse : IResponse
    {
        public Task<ResponseResult> SendAsync(ITelegramBotClient botClient, ChatId chatId,
            CancellationToken cancellationToken = default)
        {
            var result = ResponseResult.Empty;
            Sent?.Invoke(this, new ResponseSentEventArgs(result));
            return Task.FromResult(result);
        }

        public event EventHandler<ResponseSentEventArgs> Sent;
    }
}