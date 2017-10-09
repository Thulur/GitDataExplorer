using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using GitDataExplorer.Commands;
using GitDataExplorer.Files;
using GitDataExplorer.Results;
using GitDataExplorer.Results.Commits;

namespace GitDataExplorer.Windows
{
    /// <summary>
    /// Interaction logic for CommitsWindow.xaml
    /// </summary>
    partial class CommitsWindow : Window, ICommandWindow
    {
        private ListSimpleCommitsResult listResult;

        public CommitsWindow(CommandFactory commandFactory)
        {
            InitializeComponent();

            CommandFactory = commandFactory;
        }

        public CommitsWindow(CommandFactory commandFactory, IFile file)
        {
            InitializeComponent();

            CommandFactory = commandFactory;
            Title = "History: " + file.Path;
            File = file;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadFileCommits();
        }

        public CommandFactory CommandFactory { get; set; }

        public ICommit SelectedCommit { get; private set; }

        public IFile File { get; private set; }

        /// <summary>
        /// This event is called, if the DataGrid shows a list of SimpleCommitResults.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimpleCommitDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            var selectedSimpleCommit = ((DataGrid)sender).SelectedItem as SimpleCommitResult;

            if (selectedSimpleCommit == null) return;

            SelectedCommit = selectedSimpleCommit;
            DialogResult = true;

            if (File != null)
            {
                File.CommitId = selectedSimpleCommit.Id;

                foreach (var simpleCommitResult in listResult.Commits)
                {
                    if (simpleCommitResult == selectedSimpleCommit) break;

                    if (simpleCommitResult.FilesChanged.Count == 0) continue;

                    File oldVersion = simpleCommitResult.FilesChanged[0];
                    if (oldVersion.FileState == FileState.RENAMED)
                    {
                        File.Path = oldVersion.Path;
                    }
                }
            }
        }

        private async void LoadFileCommits()
        {
            if (File != null)
            {
                var command = CommandFactory.GetCommand<ListSimpleCommitsResult>();
                command.Parameters += $" --name-status --follow -- \"{File.Path}\"";
                await Task.Run(() => command.Execute());

                listResult = command.Result;
                dataGrid.ItemsSource = listResult.Commits;
            }
        }
    }
}
