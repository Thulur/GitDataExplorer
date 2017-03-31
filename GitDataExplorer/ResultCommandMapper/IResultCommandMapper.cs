using System;

using GitDataExplorer.Results;

namespace GitDataExplorer.ResultCommandMapper
{
    public interface IResultCommandMapper
    {
        string GetCommandName<ResultType>() where ResultType : IResult;

        string GetCommandParameters<ResultType>() where ResultType : IResult;

        Type GetCommandType<ResultType>() where ResultType : class, IResult, new();

        string GetOptionalParameter(OptionalParameter optionalParameter);
    }
}
