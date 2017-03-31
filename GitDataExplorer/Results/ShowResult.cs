using System;
using System.Collections.Generic;

namespace GitDataExplorer.Results
{
    class ShowResult : IResult
    {
        public ExecutionResult ExecutionResult
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void ParseResult(IList<string> lines)
        {
            throw new NotImplementedException();
        }
    }
}
