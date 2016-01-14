using System;
using System.Collections.Generic;

using GitStatisticsAnalyzer.Commands;
using GitStatisticsAnalyzer.Results;


namespace GitStatisticsAnalyzer.ResultCommandMapper
{
    class BaseResultCommandMapper : IResultCommandMapper
    {
        public BaseResultCommandMapper()
        { 
            commands = new Dictionary<Type, string>();
            parameters = new Dictionary<Type, string>();

            commands.Add(typeof(ListSimpleCommitsResult), "log");
            commands.Add(typeof(LogResult), "log");
            commands.Add(typeof(StatusResult), "status");
            commands.Add(typeof(VersionResult), "--version");

            parameters.Add(typeof(ListSimpleCommitsResult), "--oneline");
            parameters.Add(typeof(LogResult), "");
            parameters.Add(typeof(StatusResult), "");
            parameters.Add(typeof(VersionResult), "");
        }

        public string GetCommandName<ResultType>() where ResultType : IResult
        {
            return commands[typeof(ResultType)];
        }

        public string GetCommandParameters<ResultType>() where ResultType : IResult
        {
            return parameters[typeof(ResultType)];
        }

        public Type GetCommandType<ResultType>() where ResultType : class, IResult, new()
        {
            return typeof(GitCommand<ResultType>);
        }

        private readonly Dictionary<Type, string> commands = null;
        private readonly Dictionary<Type, string> parameters = null;
    }
}
