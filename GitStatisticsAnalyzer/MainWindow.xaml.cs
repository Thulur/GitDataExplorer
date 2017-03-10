using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using GitStatisticsAnalyzer.Commands;
using GitStatisticsAnalyzer.ResultCommandMapper;
using GitStatisticsAnalyzer.Results;
using GitStatisticsAnalyzer.Results.Commits;
using GitStatisticsAnalyzer.Views;

namespace GitStatisticsAnalyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow : Window
    {
        // git rev-parse --show-toplevel                                    Shows base dir of the repo
        // git log --pretty=format: --name-only --diff-filter=A             All files that ever existed in the repository
        // git log --oneline -- <path/file>                                 Get all commits a file is part of

        public MainWindow()
        {
            InitializeComponent();

            // The version command does not need a repository path
            var versionCommand = new CommandFactory(resultCommandMapper, "").GetCommand<VersionResult>();
            versionCommand.Execute();
            Title += " (Git-Version: " + versionCommand.Result + ")";
        }

        private CommandFactory commandFactory = null;
        private readonly IResultCommandMapper resultCommandMapper = new BaseResultCommandMapper();

        private async void SelectRepoButtonClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    commandFactory = new CommandFactory(resultCommandMapper, dialog.FileName);
                    var statusCommand = commandFactory.GetCommand<StatusResult>();
                    statusCommand.Execute();

                    if (statusCommand.Result.ExecutionResult == ExecutionResult.NoRepository)
                    {
                        commandFactory = null;
                        MessageBox.Show("The selected file contains no git repository.", "Error!", MessageBoxButton.OK);
                    }
                    else
                    {
                        currentBranch.Text = "Current branch: " + statusCommand.Result.CurrentBranch;
                    }

                    await ListNormalCommits();
                }
            }
        }

        private void SimpleCommitDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            var selectedSimpleCommit = ((DataGrid) sender).SelectedItem as SimpleCommitResult;
            Debug.Assert(selectedSimpleCommit != null);
            new FullCommitView(commandFactory, selectedSimpleCommit?.Id).Show();
        }

        private async void DanglingCommitButtonClick(object sender, RoutedEventArgs e)
        {
            var danglingCommand = commandFactory.GetCommand<DanglingCommitResult>();
            await Task.Run(() => danglingCommand.Execute());

            dataGrid.ItemsSource = danglingCommand.Result.Commits;
            ReconfigureEventHandlers(BareCommitDoubleClicked);
        }

        private void BareCommitDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            var selectedSimpleCommit = ((DataGrid)sender).SelectedItem as BareCommitResult;
            Debug.Assert(selectedSimpleCommit != null);
            new FullCommitView(commandFactory, selectedSimpleCommit?.Id).Show();
        }

        private async void NormalCommitButtonClick(object sender, RoutedEventArgs e)
        {
            await ListNormalCommits();

            ReconfigureEventHandlers(SimpleCommitDoubleClicked);
        }

        private void ReconfigureEventHandlers(MouseButtonEventHandler mouseDoubleClicked)
        {
            dataGrid.MouseDoubleClick -= BareCommitDoubleClicked;
            dataGrid.MouseDoubleClick -= SimpleCommitDoubleClicked;

            dataGrid.MouseDoubleClick += mouseDoubleClicked;
        }

        private async Task ListNormalCommits()
        {
            var oneLineCommand = commandFactory.GetCommand<ListSimpleCommitsResult>();
            await Task.Run(() => oneLineCommand.Execute());

            dataGrid.ItemsSource = oneLineCommand.Result.Commits;
        }
    }
}
