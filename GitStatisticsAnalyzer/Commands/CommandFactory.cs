using System;

using GitStatisticsAnalyzer.ResultCommandMapper;
using GitStatisticsAnalyzer.Results;

namespace GitStatisticsAnalyzer.Commands
{
    public class CommandFactory
    {
        private readonly string workingDir;

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

            return newCommand;
        }

        public IResultCommandMapper ResultCommandMapper { get; set; }
    }
}
