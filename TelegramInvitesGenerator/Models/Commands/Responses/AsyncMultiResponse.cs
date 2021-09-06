using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Extensions;
using TelegramInvitesGenerator.Models.Commands.Responses.Enums;
using TelegramInvitesGenerator.Models.Commands.Responses.Results;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class AsyncMultiResponse : IResponse
    {
        private readonly IObservable<IResponse> _responses;
        private readonly MultiResponseType _multiResponseType;

        public AsyncMultiResponse(IObservable<IResponse> responses,
            MultiResponseType multiResponseType = MultiResponseType.Many)
        {
            _responses = responses;
            _multiResponseType = multiResponseType;
        }

        public async Task<ResponseResult> SendAsync(ITelegramBotClient botClient, ChatId chatId,
            CancellationToken cancellationToken = default)
        {
            var messages = new List<Message>();
            ResponseResult firstResult = null;
            await _responses.ForEachAsync(async response =>
            {
                if (_multiResponseType == MultiResponseType.Single
                    && firstResult != null
                    && response is TextResponse textResponse)
                {
                    var message = firstResult.Messages.FirstOrDefault();
                    if (message != null)
                    {
                        await TelegramApi.ExecuteAsync(
                            botClient.EditMessageTextAsync(chatId, message.MessageId, textResponse.Text,
                                cancellationToken: cancellationToken));
                    }
                }
                else
                {
                    var result = await response.SendAsync(botClient, chatId, cancellationToken);
                    firstResult ??= result;
                    messages.AddRange(result.Messages);
                }
            }, cancellationToken);

            return new ResponseResult(messages);
        }
    }
}