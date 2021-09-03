using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TelegramInvitesGenerator.Models.Commands;
using TelegramInvitesGenerator.Models.Commands.BotCommands;
using TelegramInvitesGenerator.Services.Abstractions;

namespace TelegramInvitesGenerator.Services
{
    public class BotCommandsRepository : IBotCommandsRepository
    {
        private readonly IChannelInvitesGenerator _channelInvitesGenerator;

        public BotCommandsRepository(IChannelInvitesGenerator channelInvitesGenerator)
        {
            _channelInvitesGenerator = channelInvitesGenerator;
        }
        
        public IReadOnlyCollection<IBotCommand> Commands => 
            new ReadOnlyCollection<IBotCommand>(new List<IBotCommand>
        {
            new HelpBotCommand(),
            new InvitesBotCommand(_channelInvitesGenerator)
        });

        public IBotCommand GetCommand(string question) =>
            Commands.FirstOrDefault(command => command.Question.IsMatch(question));
    }
}