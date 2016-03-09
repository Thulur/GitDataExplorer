using System.Collections.Generic;

namespace GitStatisticsAnalyzer.Results
{
    public enum ExecutionResult
    {
        NotExecuted,
        Success,
        Error,
        NoRepository
    }

    public interface IResult
    {
        void ParseResult(IList<string> lines);

        ExecutionResult ExecutionResult { get; }
    }
}
