using GitStatisticsAnalyzer.Windows;

namespace GitStatisticsAnalyzer.Windows
{
    interface ICommitWindow : ICommandWindow
    { 
        string Id { get; }
    }
}
