using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GitStatisticsAnalyzer.Results;

namespace GitStatisticsAnalyzerUnitTests
{
    [TestClass]
    public class GitVersionTests
    {
        public GitVersionTests()
        {

        }

        [TestMethod]
        public void TestExistingGitVersion()
        {
            VersionResult versionResult = new VersionResult();
            //versionResult.ParseResult();
        }

        [TestMethod]
        public void TestNoGitInstalled()
        {

        }
    }
}
