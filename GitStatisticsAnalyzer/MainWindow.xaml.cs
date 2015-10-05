using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;

using GitStatisticsAnalyzer.Commands;
using GitStatisticsAnalyzer.Results;

namespace GitStatisticsAnalyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            IGitCommand version = CommandFactory.GetVersionCommand();
            versionTextBlock.Text = "Git-Version: " + version.GetResult().ToString();
        }

        private CommandFactory commandFactory = null;

        private void SelectRepoButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.ShowDialog();
            commandFactory = new CommandFactory(dialog.FileName);
            var status = commandFactory.GetStatusCommand().GetResult() as StatusResult;

            currentBranchText.Text = status?.CurrentBranch;
        }
    }
}
