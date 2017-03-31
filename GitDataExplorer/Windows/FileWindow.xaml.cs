using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

using GitDataExplorer.Commands;
using GitDataExplorer.Files;
using GitDataExplorer.Results;

namespace GitDataExplorer.Windows
{
    /// <summary>
    /// Interaction logic for FileWindow.xaml
    /// </summary>
    public partial class FileWindow : Window, ICommandWindow
    {
        private IFile file;

        static FileWindow()
        {
            FileEndingsHighlighting.Add("Java", new List<string>{ ".java" });
            FileEndingsHighlighting.Add("C#", new List<string> { ".cs" });
            FileEndingsHighlighting.Add("JavaScript", new List<string> { ".js" });
            FileEndingsHighlighting.Add("PHP", new List<string> { ".php" });
            FileEndingsHighlighting.Add("HTML", new List<string> { ".html", ".htm" });
            FileEndingsHighlighting.Add("C++", new List<string> { ".cpp", ".cc", ".h" });
        }

        public FileWindow(CommandFactory commandFactory, IFile file)
        {
            InitializeComponent();

            CommandFactory = commandFactory;
            File = file;
            Title = File.Path;
        }

        public IFile File
        {
            get
            {
                return file;
            }
            private set
            {
                file = value;
                FileUpdated();
            }
        }

        private void FileUpdated()
        {
            if (CommandFactory == null) return;

            var command = CommandFactory.GetCommand<FileResult>($"{File.CommitId}:{File.Path}");
            var extension = Path.GetExtension(File.Path);
            var language = FileEndingsHighlighting.Where(s => s.Value.Contains(extension)).FirstOrDefault().Key;
            command.Execute();
            textEditor.Text = String.Join(Environment.NewLine, command.Result.Lines);

            if (language == null) return;

            textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(language);
        }

        public CommandFactory CommandFactory { get; set; }

        public static readonly Dictionary<string, IList<String>> FileEndingsHighlighting = new Dictionary<string, IList<string>>();
    }
}
