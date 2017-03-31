using System;
using System.Windows;

using GitStatisticsAnalyzer.Commands;
using GitStatisticsAnalyzer.Files;
using GitStatisticsAnalyzer.Results;

namespace GitStatisticsAnalyzer.Windows
{
    /// <summary>
    /// Interaction logic for FileWindow.xaml
    /// </summary>
    public partial class FileWindow : Window, ICommandWindow
    {
        private IFile file;

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
            command.Execute();
            textEditor.Text = String.Join(Environment.NewLine, command.Result.Lines);
        }

        public CommandFactory CommandFactory { get; set; }
    }
}
