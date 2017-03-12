using GitStatisticsAnalyzer.Results;
using GitStatisticsAnalyzer.Results.Commits;

namespace GitStatisticsAnalyzer.ResultCommandMapper
{
    class BranchResultCommandMapper : BaseResultCommandMapper
    {
        public BranchResultCommandMapper(string branch)
        {
            parameters.Add(typeof(ListSimpleCommitsResult), branch + " --oneline");
            parameters.Add(typeof(FullCommitResult), branch);
            parameters.Add(typeof(LogResult), branch);
        }
    }
}
