using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramInvitesGenerator.Models.Commands.Answers
{
    [Obsolete("Not implemented", true)]
    public class EditMessageResponse : IResponse
    {
        public Task SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}