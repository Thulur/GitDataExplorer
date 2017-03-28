namespace GitStatisticsAnalyzer.Files
{
    class File : IFile
    {
        public File (string commitId, string path)
        {
            CommitId = commitId;
            Path = path;
        }

        public string CommitId
        {
            get;
        }

        public string Path
        {
            get;
        }
    }
}
