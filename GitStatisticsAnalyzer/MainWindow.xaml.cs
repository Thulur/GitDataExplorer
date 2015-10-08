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
        // git rev-parse --show-toplevel 

        public MainWindow()
        {
            InitializeComponent();

            // The version command does not need a repository path
            var versionCommand = new CommandFactory(resultCommandMapper, "").GetCommand<VersionResult>();
            versionTextBlock.Text = "Git-Version: " + versionCommand.Result.ToString();
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

            currentBranchText.Text = statusCommand.Result.CurrentBranch;
        }
    }
}
