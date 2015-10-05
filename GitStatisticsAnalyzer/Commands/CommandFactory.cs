using GitStatisticsAnalyzer.Commands;

namespace GitStatisticsAnalyzer.Commands
{
    class CommandFactory
    {
        public CommandFactory(string workingDir = "")
        {
            this.workingDir = workingDir;
        }

        public static IGitCommand GetVersionCommand()
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
