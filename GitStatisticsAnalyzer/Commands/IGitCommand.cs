using System.Collections.Generic;

namespace GitStatisticsAnalyzer.Commands
{
    public interface IGitCommand
    {
        void Execute();

        int LineCount { get; }

        IList<string> Lines { get; }

        string Parameters { get; set; }
    }

    public interface IGitCommand<T> : IGitCommand
    {
        T Result { get; }
    }
}
