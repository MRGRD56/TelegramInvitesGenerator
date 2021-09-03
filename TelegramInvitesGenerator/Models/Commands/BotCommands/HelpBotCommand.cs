using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramInvitesGenerator.Models.Commands.Answers;
using TelegramInvitesGenerator.Models.Commands.Questions;

namespace TelegramInvitesGenerator.Models.Commands.BotCommands
{
    public class HelpBotCommand : IBotCommand
    {
        public IQuestion Question =>
            new RegexQuestion(new Regex(@"^((\/start)|(\/help)|(start)|(начать))$", RegexOptions.IgnoreCase));

        public Task<IAnswer> GetAnswerAsync(BotMessage message)
        {
            var chatType = message?.Message.Chat.Type;
            var info = chatType == ChatType.Private
                ? "Для работы бота необходимо пригласить его в чат.\n" +
                  "В групповом чате сообщения, адресованные боту, должны начинаться с @okei_invites_generator_2021bot или @okeibot"
                : "Для генерации пригласительных ссылок отправьте команду, а также список людей в формате:\n\n" +
                  "/generate_invites\n" +
                  "Иванов Иван Иванович\n" +
                  "Соколова Анна Игоревна\n" +
                  "Ефремов Сергей Николаевич";
            IAnswer answer = new TextAnswer($"Тип чата: {chatType.ToString()}\n{info}");

            return Task.FromResult(answer);
        }
    }
}