using GitStatisticsAnalyzer.Models.Interfaces;

namespace GitStatisticsAnalyzer.Models
{
    class StatusResult : IResult
    {
        public ExecutionResult ExecutionResult { get; set; }

        public string CurrentBranch { get; set; }
    }
}
