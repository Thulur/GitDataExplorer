using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using GitStatisticsAnalyzer.Commands;
using GitStatisticsAnalyzer.Dialogs;
using GitStatisticsAnalyzer.Graphs;
using GitStatisticsAnalyzer.ResultCommandMapper;
using GitStatisticsAnalyzer.Results;
using GitStatisticsAnalyzer.Results.Commits;
using GitStatisticsAnalyzer.Windows;

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

        private CommandFactory commandFactory = null;
        private readonly IResultCommandMapper resultCommandMapper = new BaseResultCommandMapper();

        public MainWindow()
        {
            InitializeComponent();

            // The version command does not need a repository path
            var versionCommand = new CommandFactory(resultCommandMapper, "").GetCommand<VersionResult>();
            versionCommand.Execute();
            Title += " (Git-Version: " + versionCommand.Result + ")";
        }

        private async void SelectRepoButtonClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;

                if (dialog.ShowDialog() != CommonFileDialogResult.Ok) return;

                danglingCommitButton.IsEnabled = true;
                simpleCommitButton.IsEnabled = true;
                commitDiffButton.IsEnabled = true;
                authorCommitsButton.IsEnabled = true;
                linesCommitButton.IsEnabled = true;

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

        /// <summary>
        /// After calling this event all dangling commits inside the local repository are listed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DanglingCommitButtonClick(object sender, RoutedEventArgs e)
        {
            var danglingCommand = commandFactory.GetCommand<DanglingCommitResult>();
            await Task.Run(() => danglingCommand.Execute());

            dataGrid.ItemsSource = danglingCommand.Result.Commits;
            ReconfigureEventHandlers(BareCommitDoubleClicked);
        }

        private async void NormalCommitButtonClick(object sender, RoutedEventArgs e)
        {
            await ListNormalCommits();

            ReconfigureEventHandlers(SimpleCommitDoubleClicked);
        }

        private async void CommitDiffButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new BranchesDialog();
            if (dialog.ShowDialog() ?? false)
            {
                await ListNormalCommits($"{dialog.FirstBranch}..{dialog.SecondBranch}");

                ReconfigureEventHandlers(SimpleCommitDoubleClicked);
            }
        }

        /// <summary>
        /// This event is called, if the DataGrid shows a list of SimpleCommitResults.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimpleCommitDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            var selectedSimpleCommit = ((DataGrid)sender).SelectedItem as SimpleCommitResult;

            if (selectedSimpleCommit == null) return;

            new FullCommitWindow(commandFactory, selectedSimpleCommit?.Id).Show();
        }

        /// <summary>
        /// This event is called, if the DataGrid shows a list of BareCommitResult.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BareCommitDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            var selectedSimpleCommit = ((DataGrid)sender).SelectedItem as BareCommitResult;
            Debug.Assert(selectedSimpleCommit != null);
            new FullCommitWindow(commandFactory, selectedSimpleCommit?.Id).Show();
        }

        private void ReconfigureEventHandlers(MouseButtonEventHandler mouseDoubleClicked)
        {
            dataGrid.MouseDoubleClick -= BareCommitDoubleClicked;
            dataGrid.MouseDoubleClick -= SimpleCommitDoubleClicked;

            dataGrid.MouseDoubleClick += mouseDoubleClicked;
        }

        /// <summary>
        /// Asynchronously retrieves the list of all normal commits.
        /// </summary>
        /// <returns></returns>
        private async Task ListNormalCommits(string customParameters = "")
        {
            var oneLineCommand = commandFactory.GetCommand<ListSimpleCommitsResult>(customParameters);
            await Task.Run(() => oneLineCommand.Execute());

            dataGrid.ItemsSource = oneLineCommand.Result.Commits;
        }

        private void AuthorCommitsButtonClick(object sender, RoutedEventArgs e)
        {
            var window = new AuthorCommitGraphWindow { CommandFactory = commandFactory };
            window.Show();
        }
    }
}
