using System;

namespace GitDataExplorer.Results
{
    interface ICommit : IResult
    {
        string Id { get; }
    }
}
