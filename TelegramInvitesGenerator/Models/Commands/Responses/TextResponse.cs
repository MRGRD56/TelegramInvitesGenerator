using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Models.Commands.Responses.Messages;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class TextResponse : ITextResponse
    {
        public string Text { get; }

        public TextResponse(string text)
        {
            Text = text;
        }
        
        public async Task<ResponseResult> SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            var result = new ResponseResult(
                await botClient.SendTextMessageAsync(chatId, Text, cancellationToken: cancellationToken));
            Sent?.Invoke(this, new ResponseSentEventArgs(result));
            return result;
        }

        public event EventHandler<ResponseSentEventArgs> Sent;
    }
}