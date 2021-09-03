using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace TelegramInvitesGenerator.Models.Commands.Answers
{
    public class FileAnswer : ITextAnswer
    {
        public string Text { get; }
        
        public Stream FileStream { get; }
        
        private readonly string _fileName;

        public FileAnswer(byte[] fileBytes, string fileName, string text = null)
        {
            FileStream = new MemoryStream(fileBytes);
            _fileName = fileName;
            Text = text;
        }

        public FileAnswer(Stream fileStream, string fileName, string text = null)
        {
            FileStream = fileStream;
            _fileName = fileName;
            Text = text;
        }
        
        public async Task SendAsync(ITelegramBotClient botClient, ChatId chatId)
        {
            var file = new InputOnlineFile(FileStream, _fileName);
            await botClient.SendDocumentAsync(chatId, file, caption: Text);
        }
    }
}