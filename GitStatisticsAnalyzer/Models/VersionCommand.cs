using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using GitStatisticsAnalyzer.Models.Interfaces;

namespace GitStatisticsAnalyzer.Models
{
    class VersionCommand : GitCommand
    {
        public VersionCommand()
        {
            InitCommand("--version");
        }
        
        protected override void ParseResult()
        {
            LineCount = 1;          

            if (Lines.Count != 1) throw new Exception("Unexpected number of lines detected.");

            string versionLine = Lines[0];

            if (Regex.IsMatch(versionLine, @"git version [0-9]\.[0-9]\.[0-9]"))
            {
                string versionString = Regex.Match(versionLine, @"[0-9]\.[0-9]\.[0-9]").ToString();
                IList<string> versionNumbers = versionString.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                result = new VersionResult(Convert.ToByte(versionNumbers[0]), Convert.ToByte(versionNumbers[1]), Convert.ToByte(versionNumbers[2]));
                result.ExecutionResult = ExecutionResult.Success;
            }
        }
    }
}
