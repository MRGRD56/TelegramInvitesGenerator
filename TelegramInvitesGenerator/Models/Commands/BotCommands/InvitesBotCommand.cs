using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using TelegramInvitesGenerator.Models.Commands.Responses;
using TelegramInvitesGenerator.Models.Commands.Requests;
using TelegramInvitesGenerator.Models.Commands.Responses.Enums;
using TelegramInvitesGenerator.Services.Abstractions;

namespace TelegramInvitesGenerator.Models.Commands.BotCommands
{
    public class InvitesBotCommand : IBotCommand
    {
        private readonly IChannelInvitesGenerator _channelInvitesGenerator;
        private readonly IDocumentGenerator _documentGenerator;

        public InvitesBotCommand(IChannelInvitesGenerator channelInvitesGenerator, IDocumentGenerator documentGenerator)
        {
            _channelInvitesGenerator = channelInvitesGenerator;
            _documentGenerator = documentGenerator;
        }

        public IRequest Request => new ConditionRequest(q => q.StartsWith("/generate_invites"));

        private IObservable<IResponse> GetInvitesAnswers(BotMessage message, List<string> persons) =>
            Observable.Create<IResponse>(async subscriber =>
            {
                var infoMessage = $"Генерация пригласительных ссылок (количество: {persons.Count})";
                Action<string> alert = alertMessage =>
                {
                    alertMessage = $"{infoMessage}\n{alertMessage}";
                    subscriber.OnNext(new TextResponse(alertMessage));
                };
                    
                subscriber.OnNext(new TextResponse($"{infoMessage}\nПрогресс: 0%"));
                    
                var inviteLinks = await _channelInvitesGenerator
                    .GenerateAsync(message.Message.Chat.Id, persons, alert)
                    .ToListAsync();
                var document = await _documentGenerator.GenerateFromObjectsAsync(inviteLinks);
                
                subscriber.OnNext(new FileResponse(document, "invites.xlsx", $"Сгенерировано ссылок: {inviteLinks.Count}"));
            });

        public async Task<IResponse> GetResponseAsync(BotMessage message)
        {
            if (message.Message.Chat.Type == ChatType.Private)
            {
                return new TextResponse("Невозможно создать пригласительные ссылки для приватного чата.\n" +
                                      "/help для дополнительной информации.");
            }

            var match = Regex.Match(message.Text, @"^\/generate_invites(.*)", 
                RegexOptions.Singleline | RegexOptions.Multiline);
            if (!match.Success)
            {
                return new NoResponse();
            }
            var text = match.Groups[1].Value.Trim();
                
            var persons = text
                .Split("\n")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToList();

            if (!persons.Any())
            {
                return new TextResponse("Список людей для приглашения пуст.\n" +
                                      "/help для дополнительной информации.");
            }

            return new AsyncMultiResponse(GetInvitesAnswers(message, persons), MultiResponseType.Single);
        }
    }
}