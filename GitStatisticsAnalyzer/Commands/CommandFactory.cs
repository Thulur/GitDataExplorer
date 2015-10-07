using GitStatisticsAnalyzer.ResultCommandMapper;
using GitStatisticsAnalyzer.Results;
using System;

namespace GitStatisticsAnalyzer.Commands
{
    class CommandFactory
    {
        public CommandFactory(IResultCommandMapper resultCommandMapper, string workingDir)
        {
            this.workingDir = workingDir;
            ResultCommandMapper = resultCommandMapper;
        }

        public IGitCommand<ResultType> GetCommand<ResultType>() where ResultType : class, IResult, new()
        {
            var commandName = ResultCommandMapper.GetCommandName<ResultType>();
            var commandType = ResultCommandMapper.GetCommandType<ResultType>();
            var newInstance = Activator.CreateInstance(commandType, commandName, workingDir);
            var newCommand = (IGitCommand<ResultType>)newInstance;

            newCommand.Parameters = ResultCommandMapper.GetCommandParameters<ResultType>();
            newCommand.RunCommand();

            return newCommand;
        }

        public IResultCommandMapper ResultCommandMapper { get; }

        private readonly string workingDir;
    }
}
