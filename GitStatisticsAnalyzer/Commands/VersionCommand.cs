using System;

using GitStatisticsAnalyzer.Results;

namespace GitStatisticsAnalyzer.Commands
{
    class VersionCommand : BaseGitCommand<VersionResult>
    {
        public VersionCommand(string path) : base(path)
        {
            InitCommand("--version");
        }
        
        protected override void CreateResult()
        {
            LineCount = 1;          

            if (Lines.Count != 1) throw new Exception("Unexpected number of lines detected.");

            Result = new VersionResult();
            Result.ParseResult(Lines);
        }
    }
}
