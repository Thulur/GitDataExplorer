using System;

using GitStatisticsAnalyzer.Results;

namespace GitStatisticsAnalyzer.ResultCommandMapper
{
    public interface IResultCommandMapper
    {
        string GetCommandName<ResultType>() where ResultType : IResult;

        string GetCommandParameters<ResultType>() where ResultType : IResult;

        Type GetCommandType<ResultType>() where ResultType : class, IResult, new();
    }
}
