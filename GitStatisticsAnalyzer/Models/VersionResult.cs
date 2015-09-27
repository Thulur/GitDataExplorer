using System;

using GitStatisticsAnalyzer.Models.Interfaces;

namespace GitStatisticsAnalyzer.Models
{
    class VersionResult : IResult
    {
        public VersionResult(byte major, byte minor, byte bugfix)
        {
            Major = major;
            Minor = minor;
            Bugfix = bugfix;
        }

        public override bool Equals(object obj)
        {
            VersionResult versionResult = obj as VersionResult;

            if (versionResult == null) return false;

            return Major == versionResult.Major && Minor == versionResult.Minor && Bugfix == versionResult.Bugfix;
        }

        public override int GetHashCode() => Major.GetHashCode() ^ Minor.GetHashCode() ^ Bugfix.GetHashCode();

        public override string ToString() => Major.ToString() + "." + Minor.ToString() + "." + Bugfix.ToString();

        public byte Major { get; }

        public byte Minor { get; }

        public byte Bugfix { get; }

        public ExecutionResult ExecutionResult { get; set; }
    }
}
