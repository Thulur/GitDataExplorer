using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GitDataExplorer.Results;
using System.Collections.Generic;

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
            var versionResult = new VersionResult();
            var gitOutput = new List<string> {"git version 2.5.0"};

            versionResult.ParseResult(gitOutput);

            Assert.AreEqual(versionResult.Major, 2);
            Assert.AreEqual(versionResult.Minor, 5);
            Assert.AreEqual(versionResult.Bugfix, 0);
            Assert.AreEqual(versionResult.ExecutionResult, ExecutionResult.Success);
        }
    }
}
