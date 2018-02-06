using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

using GitDataExplorer.Commands;
using GitDataExplorer.Files;
using GitDataExplorer.Results;
using GitDataExplorer.Results.Commits;

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
        }

        public CommandFactory CommandFactory { get; set; }

        public static readonly Dictionary<string, IList<String>> FileEndingsHighlighting = new Dictionary<string, IList<string>>();

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

            var fileCommand = CommandFactory.GetCommand<FileResult>($"{File.CommitId}:\"{File.Path}\"");
            var commitCommand = CommandFactory.GetCommand<FullCommitResult>();
            commitCommand.Parameters = File.CommitId;
            fileCommand.Execute();
            commitCommand.Execute();

            Title = $"{File.Path} ({commitCommand.Result.Title})";
            textEditor.Text = String.Join(Environment.NewLine, fileCommand.Result.Lines);
            DetectLanguage();
        }

        private void HighlightingCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            if (File == null) return;

            DetectLanguage();
        }

        private void HighlightingCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            textEditor.SyntaxHighlighting = null;
        }

        private void DetectLanguage()
        {
            var extension = Path.GetExtension(File.Path);
            var language = FileEndingsHighlighting.Where(s => s.Value.Contains(extension)).FirstOrDefault().Key;

            if (language == null) return;

            textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(language);
        }

        private void ShowHistoryButtonClicked(object sender, RoutedEventArgs e)
        {
            CommitsWindow dialog = new CommitsWindow(CommandFactory, File);

            if (dialog.ShowDialog() == true)
            {
                ICommit commit = dialog.SelectedCommit;
                File = dialog.File;
                FileUpdated();
            }
        }
    }
}
