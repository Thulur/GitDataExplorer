using System;
using System.Collections.Generic;

using GitStatisticsAnalyzer.Results.Commits;

namespace GitStatisticsAnalyzer.Results
{
    class OneLineResult : IResult
    {
        public ExecutionResult ExecutionResult { get; }

        public IList<SimpleCommitResult> Commits { get; }

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
