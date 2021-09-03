using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramInvitesGenerator.Models.Commands.Answers;
using TelegramInvitesGenerator.Models.Commands.Questions;
using TelegramInvitesGenerator.Models.Documents;
using TelegramInvitesGenerator.Services;
using TelegramInvitesGenerator.Services.Abstractions;

namespace TelegramInvitesGenerator.Models.Commands.BotCommands
{
    public class InvitesBotCommand : IBotCommand
    {
        private readonly IChannelInvitesGenerator _channelInvitesGenerator;

        public InvitesBotCommand(IChannelInvitesGenerator channelInvitesGenerator)
        {
            _channelInvitesGenerator = channelInvitesGenerator;
        }

        public IQuestion Question => new ConditionQuestion(q => q.StartsWith("/generate_invites"));
        
        public async Task<IAnswer> GetAnswerAsync(BotMessage message)
        {
            try
            {
                if (message.Message.Chat.Type == ChatType.Private)
                {
                    return new TextAnswer("Невозможно создать пригласительные ссылки для приватного чата.\n" +
                                          "/help для дополнительной информации.");
                }

                var match = Regex.Match(message.Text, @"^\/generate_invites(.*)", 
                    RegexOptions.Singleline | RegexOptions.Multiline);
                if (!match.Success)
                {
                    return new NoAnswer();
                }
                var text = match.Groups[1].Value.Trim();
                
                var persons = text
                    .Split("\n")
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x.Trim())
                    .ToList();

                if (!persons.Any())
                {
                    return new TextAnswer("Список людей для приглашения пуст.\n" +
                                          "/help для дополнительной информации.");
                }
                
                var inviteLinks = await _channelInvitesGenerator
                    .GenerateAsync(message.Message.Chat.Id, persons)
                    .ToListAsync();
                var document = await ExcelDocumentGenerator.GenerateFromObjectsAsync(inviteLinks);
                return new FileAnswer(document, "invites.xlsx", $"Сгенерировано ссылок: {inviteLinks.Count}");
            }
            catch (Exception ex)
            {
                return new TextAnswer(ex.Message);
            }
        }
    }
}