using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using TelegramInvitesGenerator.Models.Commands.Responses.Messages;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class FileResponse : ITextResponse
    {
        public string Text { get; }

        public Stream FileStream { get; }

        private readonly string _fileName;

        public FileResponse(byte[] fileBytes, string fileName, string text = null)
        {
            FileStream = new MemoryStream(fileBytes);
            _fileName = fileName;
            Text = text;
        }

        public FileResponse(Stream fileStream, string fileName, string text = null)
        {
            FileStream = fileStream;
            _fileName = fileName;
            Text = text;
        }

        public async Task<ResponseResult> SendAsync(ITelegramBotClient botClient, ChatId chatId,
            CancellationToken cancellationToken = default)
        {
            var file = new InputOnlineFile(FileStream, _fileName);
            var result = new ResponseResult(
                await botClient.SendDocumentAsync(chatId, file, caption: Text, cancellationToken: cancellationToken));
            Sent?.Invoke(this, new ResponseSentEventArgs(result));
            return result;
        }

        public event EventHandler<ResponseSentEventArgs> Sent;
    }
}