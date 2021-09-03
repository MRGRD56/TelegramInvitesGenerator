using System;

namespace TelegramInvitesGenerator.Models.Commands.Questions
{
    public class ConditionQuestion : IQuestion
    {
        public Predicate<string> Predicate { get; }

        public ConditionQuestion(Predicate<string> predicate)
        {
            Predicate = predicate;
        }

        public bool IsMatch(string userQuestion) => Predicate?.Invoke(userQuestion) == true;
    }
}