using GitStatisticsAnalyzer.Results;

namespace GitStatisticsAnalyzer.Commands
{
    class StatusCommand : GitCommand
    {
        public StatusCommand(string path)
        {
            InitCommand("status", path);
        }

        protected override void ParseResult()
        {
            result = new StatusResult();
            result.ParseResult(Lines);
        }
    }
}
