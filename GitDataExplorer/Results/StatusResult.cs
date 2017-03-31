using System;
using System.Collections.Generic;

namespace GitDataExplorer.Results
{
    class StatusResult : IResult
    {
        public ExecutionResult ExecutionResult { get; private set; }

        public string CurrentBranch { get; private set; }

        public void ParseResult(IList<string> lines)
        {
            if (lines.Count > 0)
            {
                CurrentBranch = lines[0].Replace("On branch ", "");
                ExecutionResult = ExecutionResult.Success;
            }
            else
            {
                ExecutionResult = ExecutionResult.NoRepository;
            }
        }
    }
}
