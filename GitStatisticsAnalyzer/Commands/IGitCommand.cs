using System.Collections.Generic;

namespace GitStatisticsAnalyzer.Commands
{
    interface IGitCommand
    {
        void RunCommand();

        int LineCount { get; }

        IList<string> Lines { get; }

        string Parameters { get; set; }
    }

    interface IGitCommand<T> : IGitCommand
    {
        T Result { get; }
    }
}
