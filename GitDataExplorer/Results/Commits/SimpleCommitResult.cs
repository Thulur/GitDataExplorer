using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using GitDataExplorer.Files;

namespace GitDataExplorer.Results.Commits
{
    class SimpleCommitResult : ICommit
    {
        [Browsable(false)]
        [Display(AutoGenerateField = false)]
        public ExecutionResult ExecutionResult { get; }

        public string Id { get; private set; } = "";

        public string Message { get; private set; } = "";

        public List<File> FilesChanged { get; private set; } = new List<File>();

        public void ParseResult(IList<string> lines)
        {
            var idMessage = lines[0].Split(new char[] { ' ' }, 2);
            Id = idMessage[0];
            Message = idMessage[1];

            // Further lines should only contain create, rename, delete statements
            for (int i = 1; i < lines.Count; ++i)
            {
                ParseStatusLine(lines[i]);
            }
        }

        private void ParseStatusLine(string statusLine)
        {
            string[] tokens = statusLine.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
            var file = new File(Id, tokens[1]);

            switch (tokens[0][0])
            {
                case 'A':
                    file.FileState = FileState.CREATED;
                    break;
                case 'R':
                    file.FileState = FileState.RENAMED;
                    break;
                case 'D':
                    file.FileState = FileState.DELETED;
                    break;
                default:
                    break;
            }

            FilesChanged.Add(file);
        }
    }
}
