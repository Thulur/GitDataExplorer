using System.Text.RegularExpressions;

namespace GitStatisticsAnalyzer.Models
{
    class StatusCommand : GitCommand
    {
        public StatusCommand(string path)
        {
            InitCommand("status", path);
        }

        protected override void ParseResult()
        {
            StatusResult statusResult = new StatusResult();
            statusResult.CurrentBranch = Lines[0].Replace("On branch ", "");
            result = statusResult;
            result.ExecutionResult = Interfaces.ExecutionResult.Success;
        }
    }
}
