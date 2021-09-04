using System.Text.RegularExpressions;

namespace TelegramInvitesGenerator.Models.Commands.Requests
{
    public class RegexRequest : IRequest
    {
        public Regex QuestionRegex { get; }

        public RegexRequest(Regex questionRegex)
        {
            QuestionRegex = questionRegex;
        }

        public bool IsMatch(string userQuestion) => QuestionRegex.IsMatch(userQuestion);
    }
}