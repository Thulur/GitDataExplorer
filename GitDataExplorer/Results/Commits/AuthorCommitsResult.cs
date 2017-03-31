using System;
using System.Collections.Generic;

using GitDataExplorer.Data;

namespace GitDataExplorer.Results.Commits
{
    class AuthorCommitsResult : IResult
    {
        public ExecutionResult ExecutionResult => throw new NotImplementedException();

        public void ParseResult(IList<string> lines)
        {
            foreach (var line in lines)
            {
                var authorCommits = line.Split(new char[] { '\t' }, 2);

                AuthorCommits.Add(new Author(authorCommits[1]), Convert.ToInt32(authorCommits[0]));
            }
        }

        public Dictionary<Author, int> AuthorCommits { get; } = new Dictionary<Author, int>();
    }
}
