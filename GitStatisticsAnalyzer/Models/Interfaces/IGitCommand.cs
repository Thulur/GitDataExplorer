using System.Collections.Generic;

namespace GitStatisticsAnalyzer.Models.Interfaces
{
    interface IGitCommand
    {
        IResult GetResult();

        int LineCount { get; }

        IList<string> Lines { get; }
    }
}
