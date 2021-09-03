using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramInvitesGenerator.Models;
using TelegramInvitesGenerator.Models.Commands;
using TelegramInvitesGenerator.Models.Commands.Answers;
using TelegramInvitesGenerator.Services;
using TelegramInvitesGenerator.Services.Abstractions;

namespace TelegramInvitesGenerator.Controllers
{
    [Route("bot")]
    public class BotController : Controller
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IBotCommandsRepository _botCommandsRepository;

        public BotController(
            ITelegramBotClient botClient,
            IBotCommandsRepository botCommandsRepository)
        {
            _botClient = botClient;
            _botCommandsRepository = botCommandsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            var message = update?.Message ?? update?.ChannelPost;
            var botMessage = BotMessage.Parse(message);
            if (!botMessage.IsForBot) return Ok();

            if (message is not { Type: MessageType.Text }) return Ok();

            var command = _botCommandsRepository.GetCommand(botMessage.Text);
            
            IAnswer answer;
            if (command != null)
            {
                answer = await command.GetAnswerAsync(botMessage);
            }
            else
            {
                answer = IBotCommand.UnknownCommandAnswer;
            }

            await answer.SendAsync(_botClient, message.Chat.Id);
            return Ok();
        }
    }
}