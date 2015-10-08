using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;

using GitStatisticsAnalyzer.Commands;
using GitStatisticsAnalyzer.ResultCommandMapper;
using GitStatisticsAnalyzer.Results;

namespace GitStatisticsAnalyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // git rev-parse --show-toplevel                                    Shows base dir of the repo
        // git log --pretty=format: --name-only --diff-filter=A             All files that ever existed in the repository
        // git log --oneline -- <path/file>                                 Get all commits a file is part of

        public MainWindow()
        {
            InitializeComponent();

            // The version command does not need a repository path
            var versionCommand = new CommandFactory(resultCommandMapper, "").GetCommand<VersionResult>();
            Title += " (Git-Version: " + versionCommand.Result.ToString() + ")";
        }

        private CommandFactory commandFactory = null;
        private readonly IResultCommandMapper resultCommandMapper = new BaseResultCommandMapper();

        private void SelectRepoButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.ShowDialog();
            commandFactory = new CommandFactory(resultCommandMapper, dialog.FileName);
            var statusCommand = commandFactory.GetCommand<StatusResult>();
            
            if (statusCommand.Result.ExecutionResult == ExecutionResult.NoRepository)
            {
                commandFactory = null;
                MessageBox.Show("The selected file contains no git repository.", "Error!", MessageBoxButton.OK);
            }
            else
            {
                currentBranch.Text = "Current branch: " + statusCommand.Result.CurrentBranch;
            }
        }
    }
}
