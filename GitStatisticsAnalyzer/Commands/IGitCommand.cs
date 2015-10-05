using System.Collections.Generic;

using GitStatisticsAnalyzer.Results;

namespace GitStatisticsAnalyzer.Commands
{
    interface IGitCommand
    {
        IResult GetResult();

        int LineCount { get; }

        IList<string> Lines { get; }
    }
}
