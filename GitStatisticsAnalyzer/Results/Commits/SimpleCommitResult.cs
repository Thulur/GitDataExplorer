using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GitStatisticsAnalyzer.Results.Commits
{
    class SimpleCommitResult : ICommit
    {
        public ExecutionResult ExecutionResult { get; }

        public string Id { get; private set; } = "";

        public string Message { get; private set; } = "";

        public void ParseResult(IList<string> lines)
        {
            // The one line commit should only be initialized with a string looking like: <id> <message>
            Debug.Assert(lines.Count == 1);

            var idMessage = lines[0].Split(new char[] { ' ' }, 2);
            Id = idMessage[0];
            Message = idMessage[1];
        }
    }
}
