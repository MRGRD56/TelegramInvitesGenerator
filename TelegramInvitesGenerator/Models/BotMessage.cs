using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramInvitesGenerator.Models
{
    public record BotMessage(Message Message, string Text, bool IsForBot)
    {
        public static BotMessage Parse(Message message, IConfiguration configuration)
        {
            if (message is null)
            {
                return new BotMessage(null, null, false);
            }
            
            if (message.Chat.Type == ChatType.Private)
            {
                return new BotMessage(message, message.Text, true);
            }

            var botNickname = configuration["Telegram:Bot:Nickname"];
            
            var match = Regex.Match(message.Text, @$"^@{botNickname}\s*(.*)", 
                RegexOptions.Multiline | RegexOptions.Singleline);

            var messageTextGroup = match.Groups[1];
            if (match.Success && messageTextGroup.Success)
            {
                return new BotMessage(message, messageTextGroup.Value, true);
            }

            return new BotMessage(message, null, false);
        }
    }
}