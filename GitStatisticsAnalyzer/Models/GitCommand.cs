using System;
using System.Collections.Generic;
using System.Diagnostics;

using GitStatisticsAnalyzer.Models.Interfaces;

namespace GitStatisticsAnalyzer.Models
{
    /// <summary>
    /// Basic git command class, which encapsulates most of the command creation.
    /// Call InitCommand(commandName) to execute a command
    /// </summary>
    class GitCommand : IGitCommand
    {
        public IResult GetResult() => result;

        public int LineCount
        {
            get; protected set;
        }

        public IList<string> Lines
        {
            get; protected set;
        }

        protected void InitCommand(string commandName, string workingDir = "")
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.CreateNoWindow = true;
            info.RedirectStandardError = true;
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            info.FileName = "git.exe";

            Process process = new Process();
            info.Arguments = commandName;
            info.WorkingDirectory = workingDir;

            process.StartInfo = info;
            process.Start();

            string gitOuput = process.StandardOutput.ReadToEnd();

            process.WaitForExit();
            process.Close();

            Lines = gitOuput.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            ParseResult();

            if (result == null || result.ExecutionResult == ExecutionResult.NotExecuted)
            {
                throw new Exception("Command executed, but no result produced.");
            }
        }

        protected virtual void ParseResult()
        {
            throw new NotImplementedException();
        }

        protected IResult result;
    }
}
