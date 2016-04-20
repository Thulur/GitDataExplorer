using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
