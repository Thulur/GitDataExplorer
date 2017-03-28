using GitStatisticsAnalyzer.Commands;

namespace GitStatisticsAnalyzer.Windows
{
    interface ICommandWindow
    {
        CommandFactory CommandFactory { get; }
    }
}
