using System;

namespace TelegramInvitesGenerator.Models.Commands.Responses.Messages
{
    public class ResponseSentEventArgs : EventArgs
    {
        public ResponseResult ResponseResult { get; }

        public ResponseSentEventArgs(ResponseResult responseResult)
        {
            ResponseResult = responseResult;
        }
    }
}