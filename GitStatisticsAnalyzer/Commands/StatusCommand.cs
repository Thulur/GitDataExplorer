using System;
using GitStatisticsAnalyzer.Results;

namespace GitStatisticsAnalyzer.Commands
{
    class StatusCommand : BaseGitCommand<StatusResult>
    {
        public StatusCommand(string path) : base(path)
        {
            InitCommand("status");
        }



        protected override void CreateResult()
        {
            Result = new StatusResult();
            Result.ParseResult(Lines);
        }
    }
}
