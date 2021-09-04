using System;
using System.Collections.Generic;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Models;

namespace TelegramInvitesGenerator.Services.Abstractions
{
    public interface IChannelInvitesGenerator
    {
        IAsyncEnumerable<ChannelInvite> GenerateAsync(ChatId chatId, IEnumerable<string> persons, Action<string> alert = null);
    }
}