using System;
using System.Collections.Generic;
using System.Text;
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

        public string Title { get; private set; }

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
            ParseTitleAndMessage(lines);
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

        private void ParseTitleAndMessage(IList<string> lines)
        {
            Title = lines[3];

            if (lines.Count > 4)
            {
                ParseMessage(lines);
            }
        }

        private void ParseMessage(IList<string> lines)
        {
            var i = lines[4].Length > 0 ? 4 : 5;

            var messageSb = new StringBuilder();

            for (; i < lines.Count; ++i)
            {
                messageSb.Append(lines[i]);

                if (i >= lines.Count - 1) break;
                
                // Test whether the diff section of the commit starts
                if (lines[i + 1].StartsWith("diff --git")) break;

                messageSb.Append("\n");
            }

            Message = messageSb.ToString();
        }
    }
}
