using System.Collections.Generic;

namespace GitStatisticsAnalyzer.Results
{
    enum ExecutionResult
    {
        NotExecuted,
        Success,
        Error,
        NoRepository
    }

    interface IResult
    {
        ExecutionResult ExecutionResult { get; }

        void ParseResult(IList<string> lines);
    }
}
