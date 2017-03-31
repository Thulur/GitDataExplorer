namespace GitDataExplorer.Files
{
    /// <summary>
    /// An object implementing the IFile Interface should have a path to the file
    /// and and commit id to identify in which state the file should be observed.
    /// </summary>
    public interface IFile
    {
        string Path { get; }

        string CommitId { get; }
    }
}
