using System;

using GitStatisticsAnalyzer.Results;

namespace GitStatisticsAnalyzer.Commands
{
    class ShowCommand : BaseGitCommand<ShowResult>
    {
        public ShowCommand(string path) : base(path)
        {

        }

        protected override void CreateResult()
        {
            throw new NotImplementedException();
        }
    }
}
