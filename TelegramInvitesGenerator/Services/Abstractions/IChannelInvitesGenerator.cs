using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Models;

namespace TelegramInvitesGenerator.Services.Abstractions
{
    public interface IChannelInvitesGenerator
    {
        IAsyncEnumerable<ChannelInvite> GenerateAsync(ChatId chatId, IEnumerable<string> persons);
    }
}