using GitDataExplorer.Commands;

namespace GitDataExplorer.Windows
{
    interface ICommandWindow
    {
        CommandFactory CommandFactory { get; }
    }
}
