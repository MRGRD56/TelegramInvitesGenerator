using System.Collections.Generic;
using TelegramInvitesGenerator.Models.Commands;

namespace TelegramInvitesGenerator.Services.Abstractions
{
    public interface IBotCommandsRepository
    {
        IReadOnlyCollection<IBotCommand> Commands { get; }

        IBotCommand GetCommand(string question);
    }
}