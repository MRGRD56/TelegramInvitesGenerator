using System;
using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace TelegramInvitesGenerator.Models.Commands.Responses.Messages
{
    public class ResponseResult
    {
        public IReadOnlyCollection<Message> Messages => _messages;
        private List<Message> _messages;

        public ResponseResult(Message message)
        {
            _messages = new List<Message> {message};
        }

        public ResponseResult(IEnumerable<Message> messages)
        {
            _messages = new List<Message>();
            _messages.AddRange(messages);
        }

        public static ResponseResult Empty => new(Array.Empty<Message>());
    }
}