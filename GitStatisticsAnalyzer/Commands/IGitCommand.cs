using System.Collections.Generic;

namespace GitStatisticsAnalyzer.Commands
{
    interface IGitCommand
    {
        int LineCount { get; }

        IList<string> Lines { get; }
    }

    interface IGitCommand<T>
    {
        int LineCount { get; }

        IList<string> Lines { get; }

        T Result { get; }
    }
}
