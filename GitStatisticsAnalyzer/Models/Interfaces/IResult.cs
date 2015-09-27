namespace GitStatisticsAnalyzer.Models.Interfaces
{
    enum ExecutionResult
    {
        NotExecuted,
        Success,
        Error,
        NoRepository
    }

    interface IResult
    {
        ExecutionResult ExecutionResult { get; set; }
    }
}
