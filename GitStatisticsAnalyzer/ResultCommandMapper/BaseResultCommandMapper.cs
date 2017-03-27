using System;
using System.Collections.Generic;

using GitStatisticsAnalyzer.Commands;
using GitStatisticsAnalyzer.Results;
using GitStatisticsAnalyzer.Results.Commits;


namespace GitStatisticsAnalyzer.ResultCommandMapper
{
    class BaseResultCommandMapper : IResultCommandMapper
    {
        public BaseResultCommandMapper()
        { 
            commands = new Dictionary<Type, string>();
            parameters = new Dictionary<Type, string>();

            commands.Add(typeof(ListSimpleCommitsResult), "log");
            commands.Add(typeof(FullCommitResult), "show");
            commands.Add(typeof(LogResult), "log");
            commands.Add(typeof(StatusResult), "status");
            commands.Add(typeof(VersionResult), "--version");
            commands.Add(typeof(DanglingCommitResult), "fsck");

            parameters.Add(typeof(ListSimpleCommitsResult), "--oneline");
            parameters.Add(typeof(FullCommitResult), "");
            parameters.Add(typeof(LogResult), "");
            parameters.Add(typeof(StatusResult), "");
            parameters.Add(typeof(VersionResult), "");
            parameters.Add(typeof(DanglingCommitResult), "--lost-found");
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

        protected readonly Dictionary<Type, string> commands = null;
        protected readonly Dictionary<Type, string> parameters = null;
    }
}
