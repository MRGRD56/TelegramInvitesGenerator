using System.Text.RegularExpressions;

namespace TelegramInvitesGenerator.Models.Commands.Questions
{
    public class RegexQuestion : IQuestion
    {
        public Regex QuestionRegex { get; }

        public RegexQuestion(Regex questionRegex)
        {
            QuestionRegex = questionRegex;
        }

        public bool IsMatch(string userQuestion) => QuestionRegex.IsMatch(userQuestion);
    }
}