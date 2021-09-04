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
    [Obsolete("Not implemented", true)]
    public class EditMessageResponse : IResponse
    {
        public Task<ResponseResult> SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public event EventHandler<ResponseSentEventArgs> Sent;
    }
}