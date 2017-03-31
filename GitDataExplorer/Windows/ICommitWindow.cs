using GitDataExplorer.Windows;

namespace GitDataExplorer.Windows
{
    interface ICommitWindow : ICommandWindow
    { 
        string Id { get; }
    }
}
