using System;

namespace GitStatisticsAnalyzer.Results
{
    interface ICommit : IResult
    {
        string Id { get; }
    }
}
