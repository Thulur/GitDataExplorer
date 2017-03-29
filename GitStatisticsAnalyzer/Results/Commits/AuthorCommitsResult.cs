using System;
using System.Collections.Generic;

namespace GitStatisticsAnalyzer.Results.Commits
{
    class AuthorCommitsResult : IResult
    {
        public ExecutionResult ExecutionResult => throw new NotImplementedException();

        public void ParseResult(IList<string> lines)
        {
            foreach (var line in lines)
            {
                var authorCommits = line.Split(new char[] { '\t' }, 2);

                AuthorCommits.Add(authorCommits[1], Convert.ToInt32(authorCommits[0]));
            }
        }

        public Dictionary<string, int> AuthorCommits { get; } = new Dictionary<string, int>();
    }
}
