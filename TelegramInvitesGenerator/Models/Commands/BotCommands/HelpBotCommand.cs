using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramInvitesGenerator.Models.Commands.Responses;
using TelegramInvitesGenerator.Models.Commands.Requests;

namespace TelegramInvitesGenerator.Models.Commands.BotCommands
{
    public class HelpBotCommand : IBotCommand
    {
        private readonly string _botNickname;

        public HelpBotCommand(IConfiguration configuration)
        {
            _botNickname = configuration["Telegram:Bot:Nickname"];
        }

        public IRequest Request =>
            new ConditionRequest(question =>
            {
                var regex = new Regex(@"^((\/start)|(\/help)|(start)|(начать))$", RegexOptions.IgnoreCase);
                return string.IsNullOrWhiteSpace(question) || regex.IsMatch(question);
            });

        public Task<IResponse> GetResponseAsync(BotMessage message)
        {
            var chatType = message?.Message.Chat.Type;
            var info = chatType == ChatType.Private
                ? "Для работы бота необходимо пригласить его в чат.\n" +
                 $"В групповом чате сообщения, адресованные боту, должны начинаться с @{_botNickname}"
                : "Для генерации пригласительных ссылок отправьте команду, а также список людей в формате:\n\n" +
                  "/generate_invites\n" +
                  "Иванов Иван Иванович\n" +
                  "Соколова Анна Игоревна\n" +
                  "Ефремов Сергей Николаевич";
            IResponse response = new TextResponse(info);

            return Task.FromResult(response);
        }
    }
}