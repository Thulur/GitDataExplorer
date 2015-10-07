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

        public string Parameters { get; set; } = "";

        protected void InitCommand(string commandName)
        {
            info.CreateNoWindow = true;
            info.RedirectStandardError = true;
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            info.FileName = "git.exe";
            info.Arguments = commandName;
            info.WorkingDirectory = workingDir;            
        }

        public virtual void RunCommand()
        {
            info.Arguments += (" " + Parameters) ?? "";

            Process process = new Process();
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
        private readonly ProcessStartInfo info = new ProcessStartInfo();
    }
}
