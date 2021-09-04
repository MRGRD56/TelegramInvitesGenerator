using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Extensions.Configuration;
using TelegramInvitesGenerator.Models.Commands;
using TelegramInvitesGenerator.Models.Commands.BotCommands;
using TelegramInvitesGenerator.Services.Abstractions;

namespace TelegramInvitesGenerator.Services
{
    public class BotCommandsRepository : IBotCommandsRepository
    {
        private readonly IChannelInvitesGenerator _channelInvitesGenerator;
        private readonly IDocumentGenerator _documentGenerator;
        private readonly IConfiguration _configuration;

        public BotCommandsRepository(
            IChannelInvitesGenerator channelInvitesGenerator, 
            IDocumentGenerator documentGenerator,
            IConfiguration configuration)
        {
            _channelInvitesGenerator = channelInvitesGenerator;
            _documentGenerator = documentGenerator;
            _configuration = configuration;
        }
        
        public IReadOnlyCollection<IBotCommand> Commands => 
            new ReadOnlyCollection<IBotCommand>(new List<IBotCommand>
        {
            new HelpBotCommand(_configuration),
            new InvitesBotCommand(_channelInvitesGenerator, _documentGenerator)
        });

        public IBotCommand GetCommand(string question) =>
            Commands.FirstOrDefault(command => command.Request.IsMatch(question));
    }
}