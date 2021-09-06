using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using TelegramInvitesGenerator.Extensions;
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

        public async IAsyncEnumerable<ChannelInvite> GenerateAsync(ChatId chatId, IEnumerable<string> persons, Action<string> alert = null)
        {
            var personsArray = persons as string[] ?? persons.ToArray();
            
            var count = 0;
            var totalCount = personsArray.Length;
            string GetProgressString() => $"Прогресс: {count} - {Math.Round((double) count / totalCount * 100, 2)}%";
            
            foreach (var person in personsArray)
            {
                ChatInviteLink inviteLink = null;

                await TelegramApi.ExecuteAsync(async () =>
                {
                    inviteLink = await _botClient.CreateChatInviteLinkAsync(chatId, memberLimit: 1);
                });

                if (inviteLink == null) continue;
                count++;

                if (alert != null && (count % 5 == 0 || count == totalCount))
                {
                    alert(GetProgressString());
                }
                yield return new ChannelInvite(person, inviteLink.InviteLink);
            }
        }
    }
}