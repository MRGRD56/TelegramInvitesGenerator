using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Models.Commands.Responses.Results;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class MultiResponse : IResponse
    {
        private readonly IResponse[] _responses;

        public MultiResponse(params IResponse[] responses)
        {
            _responses = responses;
        }
        
        public async Task<ResponseResult> SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            var messages = new List<Message>();
            foreach (var answer in _responses)
            {
                var result = await answer.SendAsync(botClient, chatId, cancellationToken);
                messages.AddRange(result.Messages);
            }

            return new ResponseResult(messages);
        }
    }
}