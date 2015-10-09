using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitStatisticsAnalyzer.Results.Commits;

namespace GitStatisticsAnalyzer.Results
{
    class ListSimpleCommitsResult : IResult
    {
        public ExecutionResult ExecutionResult { get; private set; }

        public IList<SimpleCommitResult> Commits { get; } = new List<SimpleCommitResult>();

        public void ParseResult(IList<string> lines)
        {
            foreach (var line in lines)
            {
                var tmpList = new List<string>();
                tmpList.Add(line);

                var result = new SimpleCommitResult();
                result.ParseResult(tmpList);
                Commits.Add(result);
            }
        }
    }
}
