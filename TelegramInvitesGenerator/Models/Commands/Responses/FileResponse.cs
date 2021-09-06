using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using TelegramInvitesGenerator.Extensions;
using TelegramInvitesGenerator.Models.Commands.Responses.Results;
using File = System.IO.File;

namespace TelegramInvitesGenerator.Models.Commands.Responses
{
    public class FileResponse : ITextResponse
    {
        public string Text { get; }

        public InputOnlineFile File { get; }
        private readonly Stream _fileStream;
        private readonly string _fileName;

        public FileResponse(byte[] fileBytes, string fileName, string text = null)
        {
            _fileStream = new MemoryStream(fileBytes);
            _fileName = fileName;
            Text = text;
            File = new InputOnlineFile(_fileStream, _fileName);
        }

        public FileResponse(Stream fileStream, string fileName, string text = null)
        {
            _fileStream = fileStream;
            _fileName = fileName;
            Text = text;
            File = new InputOnlineFile(_fileStream, _fileName);
        }

        private readonly string BackupsDirectory = 
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
                "invgenbot_bak", 
                DateTime.UtcNow.ToFileTimeUtc().ToString());
        
        private async Task BackupFileAsync()
        {
            if (!Directory.Exists(BackupsDirectory))
            {
                Directory.CreateDirectory(BackupsDirectory);
            }

            var filePath = Path.Combine(BackupsDirectory, _fileName);

            ResetFileStreamPosition();
            await using var newFileStream = new FileStream(filePath, FileMode.Create);
            await _fileStream.CopyToAsync(newFileStream);
        }

        private void ResetFileStreamPosition()
        {
            if (_fileStream.CanSeek)
            {
                _fileStream.Seek(0, SeekOrigin.Begin);
            }
        }
        
        public async Task<ResponseResult> SendAsync(ITelegramBotClient botClient, ChatId chatId, CancellationToken cancellationToken = default)
        {
            try
            {
                var file = new InputOnlineFile(_fileStream, _fileName);
                var message = await TelegramApi.ExecuteAsync(async () =>
                {
                    ResetFileStreamPosition();
                    return await botClient.SendDocumentAsync(chatId, file, caption: Text,
                        cancellationToken: cancellationToken);
                });

                return new ResponseResult(message);
            }
            finally
            {
                await BackupFileAsync();
            }
        }
    }
}