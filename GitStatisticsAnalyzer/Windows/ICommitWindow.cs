using GitStatisticsAnalyzer.Windows;

namespace GitStatisticsAnalyzer.Views
{
    interface ICommitWindow : ICommandWindow
    { 
        string Id { get; }
    }
}
