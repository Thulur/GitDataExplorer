using System;
using System.Collections.Generic;
using System.Text;
using GitDataExplorer.Data;
using System.Linq;
using GitDataExplorer.Files;

namespace GitDataExplorer.Results.Commits
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

        public IList<IFile> Files { get; private set; } 

        public void ParseResult(IList<string> lines)
        {
            if (lines.Count == 0)
            {
                ExecutionResult = ExecutionResult.Error;
                return;
            }

            Id = lines[0].Replace("commit ", "");

            var authorLine = 0;
            if (lines[1].StartsWith("Author"))
            {
                authorLine = 1;
            }

            if (lines[2].StartsWith("Author"))
            {
                authorLine = 2;
            }

            Author = new Author(lines[authorLine]);
            ParseDate(lines[authorLine + 1]);
            ParseTitleAndMessage(lines, authorLine + 2);
            ParseFiles(lines);
            ExecutionResult = ExecutionResult.Success;
        }

        private void ParseFiles(IList<string> lines)
        {
            var diffLines = lines.Where(l => l.StartsWith("diff --git")).ToList();
            var files = new List<IFile>();

            foreach (var diffLine in diffLines)
            {
                files.Add(new File(Id, diffLine.Split(' ').Last().Substring(2)));
            }

            Files = files;
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

        private void ParseTitleAndMessage(IList<string> lines, int startLine)
        {
            Title = lines[startLine];

            if (lines.Count > startLine + 1)
            {
                ParseMessage(lines, startLine);
            }
        }

        private void ParseMessage(IList<string> lines, int startLine)
        {
            var i = lines[startLine + 1].Length > 0 ? startLine + 1 : startLine + 2;

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
