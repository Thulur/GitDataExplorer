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
            var tmpList = new List<string>();
            foreach (var line in lines)
            {
                if (line.StartsWith("A") || line.StartsWith("M") || line.StartsWith("R"))
                {
                    tmpList.Add(line);
                }
                else
                {
                    if (tmpList.Count > 0)
                    {
                        var result = new SimpleCommitResult();
                        result.ParseResult(tmpList);
                        Commits.Add(result);
                    }
                    
                    tmpList.Clear();
                    tmpList.Add(line);
                }
            }

            if (tmpList.Count > 0)
            {
                var result = new SimpleCommitResult();
                result.ParseResult(tmpList);
                Commits.Add(result);
            }
        }
    }
}
