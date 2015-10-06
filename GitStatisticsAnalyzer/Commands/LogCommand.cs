using GitStatisticsAnalyzer.Results;

namespace GitStatisticsAnalyzer.Commands
{
    // TODO: Implement an enum with all needed parameters of git log (e.g.: numstat, author)
    class LogCommand : BaseGitCommand<LogResult>
    {
        public LogCommand(string path) : base(path)
        {

        }

        protected override void CreateResult()
        {
            
        }
    }
}
