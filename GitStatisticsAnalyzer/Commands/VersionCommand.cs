using System;

using GitStatisticsAnalyzer.Results;

namespace GitStatisticsAnalyzer.Commands
{
    class VersionCommand : GitCommand
    {
        public VersionCommand()
        {
            InitCommand("--version");
        }
        
        protected override void ParseResult()
        {
            LineCount = 1;          

            if (Lines.Count != 1) throw new Exception("Unexpected number of lines detected.");

            result = new VersionResult();
            result.ParseResult(Lines);
        }
    }
}
