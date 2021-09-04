using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Models.Commands.Responses.Messages;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class MultiResponse : IResponse
    {
        private readonly IResponse[] _answers;

        public MultiResponse(params IResponse[] answers)
        {
            _answers = answers;
        }
        
        public async Task<ResponseResult> SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            var messages = new List<Message>();
            foreach (var answer in _answers)
            {
                var responseResult = await answer.SendAsync(botClient, chatId, cancellationToken);
                messages.AddRange(responseResult.Messages);
            }

            var result = new ResponseResult(messages);
            Sent?.Invoke(this, new ResponseSentEventArgs(result));
            return result;
        }

        public event EventHandler<ResponseSentEventArgs> Sent;
    }
}