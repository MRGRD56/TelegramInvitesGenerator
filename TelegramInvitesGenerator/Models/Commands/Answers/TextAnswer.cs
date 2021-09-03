using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramInvitesGenerator.Models.Commands.Answers
{
    public class TextAnswer : ITextAnswer
    {
        public string Text { get; }

        public TextAnswer(string text)
        {
            Text = text;
        }
        
        public async Task SendAsync(ITelegramBotClient botClient, ChatId chatId)
        {
            await botClient.SendTextMessageAsync(chatId, Text);
        }
    }
}