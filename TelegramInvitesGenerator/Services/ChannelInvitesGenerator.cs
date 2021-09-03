using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Models;
using TelegramInvitesGenerator.Services.Abstractions;

namespace TelegramInvitesGenerator.Services
{
    public class ChannelInvitesGenerator : IChannelInvitesGenerator
    {
        private readonly ITelegramBotClient _botClient;

        public ChannelInvitesGenerator(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async IAsyncEnumerable<ChannelInvite> GenerateAsync(ChatId chatId, IEnumerable<string> persons)
        {
            foreach (var person in persons)
            {
                var inviteLink = await _botClient.CreateChatInviteLinkAsync(chatId, memberLimit: 1);
                yield return new ChannelInvite(person, inviteLink.InviteLink);
            }
        }
    }
}