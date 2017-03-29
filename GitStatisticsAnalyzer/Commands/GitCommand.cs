using System;
using System.Collections.Generic;
using System.Diagnostics;

using GitStatisticsAnalyzer.Results;

namespace GitStatisticsAnalyzer.Commands
{
    /// <summary>
    /// Git command class, which encapsulates most of the command creation.
    /// </summary>
    class GitCommand<T> : IGitCommand<T>, IGitCommand  where T : class, IResult, new()
    {
        public GitCommand(string commandName, string workingDir)
        {
            this.workingDir = workingDir;
            this.commandName = commandName;

            InitCommand();
        }

        public int LineCount
        {
            get; private set;
        }

        public IList<string> Lines { get; protected set; }

        public T Result { get; protected set; }

        public string Parameters { get; set; } = "";

        private void InitCommand()
        {
            info.CreateNoWindow = true;
            info.RedirectStandardError = true;
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            info.FileName = "git.exe";
            info.Arguments = commandName;
            info.WorkingDirectory = workingDir;
            info.StandardOutputEncoding = System.Text.Encoding.UTF8;

            Result = new T();
        }

        public void Execute()
        {
            info.Arguments += " " + Parameters;

            Process process = new Process {StartInfo = info};
            process.Start();

            string gitOuput = process.StandardOutput.ReadToEnd();

            process.WaitForExit();
            process.Close();

            Lines = gitOuput.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            CreateResult();
        }

        protected void CreateResult()
        {
            Result.ParseResult(Lines);
        }

        private readonly string workingDir;
        private readonly string commandName;
        private readonly ProcessStartInfo info = new ProcessStartInfo();
    }
}
