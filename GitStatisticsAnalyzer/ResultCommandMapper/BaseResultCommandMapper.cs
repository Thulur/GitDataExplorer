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
            // Commit dependent commands
            commands.Add(typeof(ListSimpleCommitsResult), "log");
            commands.Add(typeof(FullCommitResult), "show");
            commands.Add(typeof(LogResult), "log");
            
            parameters.Add(typeof(ListSimpleCommitsResult), "--oneline");
            parameters.Add(typeof(FullCommitResult), "");
            parameters.Add(typeof(LogResult), "");

            // Commit independent commands
            commands.Add(typeof(StatusResult), "status");
            commands.Add(typeof(VersionResult), "--version");
            commands.Add(typeof(DanglingCommitResult), "fsck");
            commands.Add(typeof(AuthorCommitsResult), "shortlog");

            parameters.Add(typeof(StatusResult), "");
            parameters.Add(typeof(VersionResult), "");
            parameters.Add(typeof(DanglingCommitResult), "--lost-found");
            parameters.Add(typeof(AuthorCommitsResult), "-s -n --all");

            // Optional parameters
            optionalParameters.Add(OptionalParameter.NONE, "");
            optionalParameters.Add(OptionalParameter.EXCLUDE_MERGES, "--no-merges");
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

        public string GetOptionalParameter(OptionalParameter optionalParameter)
        {
            return optionalParameters[optionalParameter];
        }

        protected readonly Dictionary<Type, string> commands = new Dictionary<Type, string>();
        protected readonly Dictionary<Type, string> parameters = new Dictionary<Type, string>();
        protected readonly Dictionary<OptionalParameter, string> optionalParameters = new Dictionary<OptionalParameter, string>();
    }

    public enum OptionalParameter
    {
        NONE,
        EXCLUDE_MERGES
    }
}
