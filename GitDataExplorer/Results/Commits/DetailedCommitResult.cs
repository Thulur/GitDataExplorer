using System;
using System.Collections.Generic;

namespace GitDataExplorer.Results.Commits
{
    /// <summary>
    /// The class stores all possible information about a commit including changes made to files.
    /// </summary>
    class DetailedCommitResult : ICommit
    {
        public ExecutionResult ExecutionResult
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void ParseResult(IList<string> lines)
        {
            throw new NotImplementedException();
        }
    }
}
