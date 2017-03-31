using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using GitDataExplorer.Results;
using GitDataExplorer.Results.Commits;
using System;

namespace GitStatisticsAnalyzerUnitTests
{
    [TestClass]
    public class FullCommitTests
    {
        [TestMethod]
        public void TestParsedFullCommit()
        {
            var fullCommit = new FullCommitResult();
            var gitOutput = new List<string>();
            var expectedTitle = "Init repository";
            var expectedMessage = "- add basic classes for the analyzer\n";
            expectedMessage += "- implement version command\n";
            expectedMessage += "- implement status command(currently only the branch is saved)\n";
            expectedMessage += "- create log command";

            gitOutput.Add("commit 94a97da2a51ee4f1b7c13370d86f2b2928e654d4");
            gitOutput.Add("Author: Sebastian Koall <sebastian.koall@student.hpi.uni-potsdam.de>");
            gitOutput.Add("Date:   Sun Sep 27 18:19:19 2015 +0200");
            gitOutput.Add("Init repository");
            gitOutput.Add("- add basic classes for the analyzer");
            gitOutput.Add("- implement version command");
            gitOutput.Add("- implement status command(currently only the branch is saved)");
            gitOutput.Add("- create log command");
            gitOutput.Add("diff --git path/to/some/file.txt");
            fullCommit.ParseResult(gitOutput);

            Assert.AreEqual("94a97da2a51ee4f1b7c13370d86f2b2928e654d4", fullCommit.Id);
            Assert.AreEqual("Sebastian Koall", fullCommit.Author.Name);
            Assert.AreEqual("sebastian.koall@student.hpi.uni-potsdam.de", fullCommit.Author.Email);
            Assert.AreEqual(new DateTime(2015, 9, 27, 18, 19, 19), fullCommit.Date);
            Assert.AreEqual(expectedTitle, fullCommit.Title);
            Assert.AreEqual(expectedMessage, fullCommit.Message);
            Assert.AreEqual(ExecutionResult.Success, fullCommit.ExecutionResult);
        }
    }
}
