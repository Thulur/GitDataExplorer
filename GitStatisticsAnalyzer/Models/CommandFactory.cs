using GitStatisticsAnalyzer.Models.Interfaces;

namespace GitStatisticsAnalyzer.Models
{
    class CommandFactory
    {
        public CommandFactory(string workingDir = "")
        {
            this.workingDir = workingDir;
        }

        public IGitCommand GetVersionCommand()
        {
            VersionCommand versionCommand = new VersionCommand();
            return versionCommand;
        }

        public IGitCommand GetStatusCommand()
        {
            StatusCommand statusCommand = new StatusCommand(workingDir);
            return statusCommand;
        }

        private string workingDir;
    }
}
