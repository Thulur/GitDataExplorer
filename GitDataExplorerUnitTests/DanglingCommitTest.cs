using System;
using System.Collections;
using System.Collections.Generic;
using GitDataExplorer.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitStatisticsAnalyzerUnitTests
{
    [TestClass]
    public class DanglingCommitTest
    {
        [TestMethod]
        public void TestExistingDanglingCommits()
        {
            var lines = new List<string>(){ "dangling blob b830997f43ab91bf5af0986d6cdfe42103c1a847",
                                            "dangling commit 3a1d2d5fdea7a895a70f07bc2f367d7d22a9a04e",
                                            "dangling blob 1ea2c7246160d9ec5c7930aff44d5520b10968a8",
                                            "dangling commit 6252cf45a4fecdf49c69399bf703a3967001018b",
                                            "dangling blob cf42915df8b55ae4722739764942c0b924a7650c"};
            var result = new DanglingCommitResult();
            result.ParseResult(lines);

            Assert.AreEqual("3a1d2d5fdea7a895a70f07bc2f367d7d22a9a04e", result.Commits[0].Id);
            Assert.AreEqual("6252cf45a4fecdf49c69399bf703a3967001018b", result.Commits[1].Id);
        }
    }
}
