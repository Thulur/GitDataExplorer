using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitDataExplorer.Results.Commits;

namespace GitDataExplorer.Results
{
    class ListSimpleCommitsResult : IResult
    {
        public ExecutionResult ExecutionResult { get; private set; }

        public IList<SimpleCommitResult> Commits { get; } = new List<SimpleCommitResult>();

        public void ParseResult(IList<string> lines)
        {
            foreach (var line in lines)
            {
                var tmpList = new List<string> {line};

                var result = new SimpleCommitResult();
                result.ParseResult(tmpList);
                Commits.Add(result);
            }
        }
    }
}
