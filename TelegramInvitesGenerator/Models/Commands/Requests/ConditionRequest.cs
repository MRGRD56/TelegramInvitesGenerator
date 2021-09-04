using System;

namespace TelegramInvitesGenerator.Models.Commands.Requests
{
    public class ConditionRequest : IRequest
    {
        public Predicate<string> Predicate { get; }

        public ConditionRequest(Predicate<string> predicate)
        {
            Predicate = predicate;
        }

        public bool IsMatch(string userQuestion) => Predicate?.Invoke(userQuestion) == true;
    }
}