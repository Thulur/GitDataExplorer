using System.Collections.Generic;

namespace GitStatisticsAnalyzer.Results
{
    class FileResult : IResult
    {
        public ExecutionResult ExecutionResult { get; private set; } = ExecutionResult.NotExecuted;

        public IEnumerable<string> Lines { get; private set; }

        public void ParseResult(IList<string> lines)
        {
            Lines = lines;
            ExecutionResult = ExecutionResult.Success;
        }
    }
}
