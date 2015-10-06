using System;
using System.Collections.Generic;
using System.Diagnostics;

using GitStatisticsAnalyzer.Results;

namespace GitStatisticsAnalyzer.Commands
{
    /// <summary>
    /// Basic git command class, which encapsulates most of the command creation.
    /// Call InitCommand(commandName) to execute a command
    /// </summary>
    abstract class BaseGitCommand<T> : IGitCommand<T>, IGitCommand where T : IResult
    {
        public BaseGitCommand(string workingDir)
        {
            this.workingDir = workingDir;
        }

        public int LineCount
        {
            get; protected set;
        }

        public IList<string> Lines { get; protected set; }

        public T Result { get; protected set; }

        protected void InitCommand(string commandName)
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
            CreateResult();
        }

        protected virtual void CreateResult()
        {
            throw new NotImplementedException();
        }

        private readonly string workingDir;
    }
}
