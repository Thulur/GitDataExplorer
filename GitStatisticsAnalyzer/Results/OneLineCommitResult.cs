using System;
using System.Collections.Generic;

namespace GitStatisticsAnalyzer.Results
{
    class OneLineCommitResult : ICommit
    {
        public ExecutionResult ExecutionResult { get; }

        public string Id { get; } = "";

        public string Message { get; } = "";

        public void ParseResult(IList<string> lines)
        {
            throw new NotImplementedException();
        }
    }
}
