using System;
using System.Collections.Generic;

namespace GitStatisticsAnalyzer.Results
{
    class StatusResult : IResult
    {
        public ExecutionResult ExecutionResult { get; private set; }

        public string CurrentBranch { get; private set; }

        public void ParseResult(IList<string> lines)
        {
            CurrentBranch = lines[0].Replace("On branch ", "");
            ExecutionResult = ExecutionResult.Success;
        }
    }
}
