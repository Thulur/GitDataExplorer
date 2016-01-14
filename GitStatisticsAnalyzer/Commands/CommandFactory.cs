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

        public IGitCommand<TResult> GetCommand<TResult>() where TResult : class, IResult, new()
        {
            var commandName = ResultCommandMapper.GetCommandName<TResult>();
            var commandType = ResultCommandMapper.GetCommandType<TResult>();
            var newInstance = Activator.CreateInstance(commandType, commandName, workingDir);
            var newCommand = (IGitCommand<TResult>)newInstance;

            newCommand.Parameters = ResultCommandMapper.GetCommandParameters<TResult>();
            newCommand.RunCommand();

            return newCommand;
        }

        public IResultCommandMapper ResultCommandMapper { get; }

        private readonly string workingDir;
    }
}
