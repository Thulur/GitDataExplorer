using Microsoft.Win32;
using System.Windows;

using GitStatisticsAnalyzer.Models;
using GitStatisticsAnalyzer.Models.Interfaces;

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

            IGitCommand version = new CommandFactory().GetVersionCommand();
            versionTextBlock.Text = "Git-Version: " + version.GetResult().ToString();
        }

        private CommandFactory commandFactory = null;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            commandFactory = new CommandFactory(@"D:\Arbeit\dpa-dbprojekt\");
            IGitCommand status = commandFactory.GetStatusCommand();
        }
    }
}
