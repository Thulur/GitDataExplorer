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
            return GetCommand<TResult>(OptionalParameter.NONE, "");
        }

        public IGitCommand<TResult> GetCommand<TResult>(OptionalParameter optionalParameter) where TResult : class, IResult, new()
        {
            return GetCommand<TResult>(optionalParameter, "");
        }

        public IGitCommand<TResult> GetCommand<TResult>(string customParameter) where TResult : class, IResult, new()
        {
            return GetCommand<TResult>(OptionalParameter.NONE, customParameter);
        }

        public IGitCommand<TResult> GetCommand<TResult>(OptionalParameter optionalParameter, string customParameter) where TResult : class, IResult, new()
        {
            var commandName = ResultCommandMapper.GetCommandName<TResult>();
            var commandType = ResultCommandMapper.GetCommandType<TResult>();
            var newInstance = Activator.CreateInstance(commandType, commandName, workingDir);
            var newCommand = (IGitCommand<TResult>)newInstance;

            var parameters = ResultCommandMapper.GetCommandParameters<TResult>();
            parameters += " " + ResultCommandMapper.GetOptionalParameter(optionalParameter);
            parameters += " " + customParameter;
            newCommand.Parameters = parameters;

            return newCommand;
        }

        public IResultCommandMapper ResultCommandMapper { get; set; }
    }
}
