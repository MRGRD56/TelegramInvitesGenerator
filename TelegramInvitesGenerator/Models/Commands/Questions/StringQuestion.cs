using System;
using System.Globalization;

namespace TelegramInvitesGenerator.Models.Commands.Questions
{
    public class StringQuestion : IQuestion
    {
        public string Text { get; }

        private readonly bool _doTrim;
        private readonly StringComparison _stringComparison;

        public StringQuestion(string text, bool doTrim = true,
            StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase)
        {
            _doTrim = doTrim;
            _stringComparison = stringComparison;
            Text = text;
        }

        public bool IsMatch(string userQuestion)
        {
            return string.Equals(Text, _doTrim ? userQuestion?.Trim() : userQuestion, _stringComparison);
        }
    }
}