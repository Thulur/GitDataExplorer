using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GitDataExplorer.Results
{
    class VersionResult : IResult
    {
        public override bool Equals(object obj)
        {
            VersionResult versionResult = obj as VersionResult;

            if (versionResult == null) return false;

            return Major == versionResult.Major && Minor == versionResult.Minor && Bugfix == versionResult.Bugfix;
        }

        public override int GetHashCode() => Major.GetHashCode() ^ Minor.GetHashCode() ^ Bugfix.GetHashCode();

        public override string ToString() => Major.ToString() + "." + Minor.ToString() + "." + Bugfix.ToString();

        public void ParseResult(IList<string> lines)
        {
            string versionLine = lines[0];

            if (Regex.IsMatch(versionLine, @"git version [0-9]\.[0-9]\.[0-9]"))
            {
                string versionString = Regex.Match(versionLine, @"[0-9]\.[0-9]\.[0-9]").ToString();
                IList<string> versionNumbers = versionString.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                Major = Convert.ToByte(versionNumbers[0]);
                Minor = Convert.ToByte(versionNumbers[1]);
                Bugfix = Convert.ToByte(versionNumbers[2]);
                ExecutionResult = ExecutionResult.Success;
            }
        }

        public byte Major { get; private set; }

        public byte Minor { get; private set; }

        public byte Bugfix { get; private set; }

        public ExecutionResult ExecutionResult { get; private set; }
    }
}
