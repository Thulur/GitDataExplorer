using System;
using GitStatisticsAnalyzer.Results;
using System.Collections.Generic;

namespace GitStatisticsAnalyzer.Commands
{
    enum CommandTypes
    {
        LogCommand,
        StatusCommand,
        VersionCommand
    }

    class CommandFactory
    {
        public CommandFactory(string workingDir = "")
        {
            this.workingDir = workingDir;

            RegisterCommandTypes();
        }

        public IGitCommand<ResultType> GetCommand<ResultType>() where ResultType : IResult
        {
            var commandType = commands[typeof(ResultType)];
            var newCommand = (IGitCommand<ResultType>)Activator.CreateInstance(commandType, workingDir);

            // TODO: A second version is needed to cover commands with additional parameters 

            return newCommand;
        }

        private void RegisterCommandTypes()
        {
            // Do not initalize the commands twice
            if (commands != null) return;

            commands = new Dictionary<Type, Type>();
            commands.Add(typeof(StatusResult), typeof(StatusCommand));
            commands.Add(typeof(VersionResult), typeof(VersionCommand));
        }

        private readonly string workingDir;
        private Dictionary<Type, Type> commands = null;
    }
}
