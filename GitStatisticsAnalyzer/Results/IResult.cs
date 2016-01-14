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
        void ParseResult(IList<string> lines);

        ExecutionResult ExecutionResult { get; }
    }
}
