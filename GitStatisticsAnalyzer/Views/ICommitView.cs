using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitStatisticsAnalyzer.Commands;

namespace GitStatisticsAnalyzer.Views
{
    public interface ICommitView
    {
        CommandFactory CommandFactory { get; }

        string Id { get; }
    }
}
