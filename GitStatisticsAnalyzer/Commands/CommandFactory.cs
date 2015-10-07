using System;
using GitStatisticsAnalyzer.Results;
using System.Collections.Generic;

namespace GitStatisticsAnalyzer.Commands
{
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
            var newCommand = (IGitCommand<ResultType>)Activator.CreateInstance(commandType.Item1, workingDir);

            newCommand.Parameters = commandType.Item2;
            newCommand.RunCommand();

            return newCommand;
        }

        private void RegisterCommandTypes()
        {
            // Do not initalize the commands twice
            if (commands != null) return;

            commands = new Dictionary<Type, Tuple<Type, string>>();
            commands.Add(typeof(StatusResult), Tuple.Create(typeof(StatusCommand), ""));
            commands.Add(typeof(VersionResult), Tuple.Create(typeof(VersionCommand), ""));
        }

        private readonly string workingDir;
        private Dictionary<Type, Tuple<Type, string>> commands = null;
    }
}
