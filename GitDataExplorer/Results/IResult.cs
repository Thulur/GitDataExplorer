using System.Collections.Generic;

namespace GitDataExplorer.Results
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
