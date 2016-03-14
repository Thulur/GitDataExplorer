using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GitStatisticsAnalyzer.Results.Commits
{
    /// <summary>
    /// A commit only consisting of its Id in order to pass around the simplest available object.
    /// </summary>
    class BareCommitResult : ICommit
    {
        public void ParseResult(IList<string> lines)
        {
            Debug.Assert(lines.Count == 1);

            Id = lines[0];
        }

        public ExecutionResult ExecutionResult { get; }
        public string Id { get; private set; }
    }
}
