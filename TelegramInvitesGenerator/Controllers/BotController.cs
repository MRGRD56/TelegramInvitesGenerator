using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramInvitesGenerator.Models;
using TelegramInvitesGenerator.Models.Commands;
using TelegramInvitesGenerator.Models.Commands.Responses;
using TelegramInvitesGenerator.Services;
using TelegramInvitesGenerator.Services.Abstractions;

namespace TelegramInvitesGenerator.Controllers
{
    [Route("bot")]
    public class BotController : Controller
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IBotCommandsRepository _botCommandsRepository;
        private readonly IConfiguration _configuration;

        public BotController(
            ITelegramBotClient botClient,
            IBotCommandsRepository botCommandsRepository,
            IConfiguration configuration)
        {
            _botClient = botClient;
            _botCommandsRepository = botCommandsRepository;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update, CancellationToken cancellationToken)
        {
            var message = update?.Message ?? update?.ChannelPost;
            var botMessage = BotMessage.Parse(message, _configuration);
            if (!botMessage.IsForBot) return Ok();

            if (message is not { Type: MessageType.Text }) return Ok();

            var command = _botCommandsRepository.GetCommand(botMessage.Text);
            
            IResponse response;
            if (command != null)
            {
                response = await command.GetResponseAsync(botMessage);
            }
            else
            {
                response = IResponse.UnknownCommandResponse;
            }

            var responseTask = response.SendAsync(_botClient, message.Chat.Id, cancellationToken);
            await Task.WhenAny(responseTask, Task.Delay(TimeSpan.FromSeconds(50), cancellationToken));
            return Ok();
        }
    }
}