namespace GitDataExplorer.Files
{
    class File : IFile
    {
        public File (string commitId, string path)
        {
            CommitId = commitId;
            Path = path;
        }

        public string CommitId { get; set; }

        public string Path { get; set; }

        public FileState FileState { get; set; } = FileState.NONE;
    }

    enum FileState
    {
        NONE,
        CREATED,
        RENAMED,
        DELETED
    }
}
