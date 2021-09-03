using System.Text.RegularExpressions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramInvitesGenerator.Models
{
    public record BotMessage(Message Message, string Text, bool IsForBot)
    {
        public static BotMessage Parse(Message message)
        {
            if (message.Chat.Type == ChatType.Private)
            {
                return new BotMessage(message, message.Text, true);
            }

            var match = Regex.Match(message.Text, @"^@okei(_invites_generator_2021)?bot\s(.*)", 
                RegexOptions.Multiline | RegexOptions.Singleline);

            var messageTextGroup = match.Groups[2];
            if (match.Success && messageTextGroup.Success)
            {
                return new BotMessage(message, messageTextGroup.Value, true);
            }

            return new BotMessage(message, null, false);
        }
    }
}