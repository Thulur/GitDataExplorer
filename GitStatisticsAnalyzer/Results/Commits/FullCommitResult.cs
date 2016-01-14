using System;
using System.Collections.Generic;

using GitStatisticsAnalyzer.Data;

namespace GitStatisticsAnalyzer.Results.Commits
{
    /// <summary>
    /// The class stores all available meta data of a commit.
    /// </summary>
    class FullCommitResult : ICommit
    { 
        public ExecutionResult ExecutionResult { get; private set; }

        public string Id { get; private set; }

        public Author Author { get; private set; }

        public DateTime Date { get; private set; }

        public string Message { get; private set; }

        public void ParseResult(IList<string> lines)
        {
            if (lines.Count == 0)
            {
                ExecutionResult = ExecutionResult.Error;
                return;
            }

            Id = lines[0].Replace("commit ", "");
            Author = new Author(lines[1]);
            ParseDate(lines[2]);
            ParseMessage(lines);
            ExecutionResult = ExecutionResult.Success;
        }

        private void ParseDate(string dateString)
        {
            var dateInfo = dateString.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var tmpDateString = dateInfo[2] + " " + dateInfo[3] + "," + dateInfo[5];
            var tmpDate = DateTime.Parse(tmpDateString);
            var timeInfo = dateInfo[4].Split(new string[] { ":" }, StringSplitOptions.None);
            var hours = Convert.ToInt32(timeInfo[0]);
            var minutes = Convert.ToInt32(timeInfo[1]);
            var seconds = Convert.ToInt32(timeInfo[2]);

            Date = new DateTime(tmpDate.Year, tmpDate.Month, tmpDate.Day, hours, minutes, seconds);
        }

        private void ParseMessage(IList<string> lines)
        {
            // Take all lines under the date and save them into the message (ignore the empty line between date and first message line)
        }
    }
}
