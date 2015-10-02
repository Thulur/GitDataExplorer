using System;

namespace GitStatisticsAnalyzer.Models.Interfaces
{
    interface ICommit : IResult
    {
        string Id { get; }
        string Author { get; }
    }
}
