using GitStatisticsAnalyzer.Commands;

namespace GitStatisticsAnalyzer.Views
{
    public interface ICommitWindow
    {
        CommandFactory CommandFactory { get; }

        string Id { get; }
    }
}
