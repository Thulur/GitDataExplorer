using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

using GitStatisticsAnalyzer.Commands;
using GitStatisticsAnalyzer.Results;

using GitStatisticsAnalyzer.Results.Commits;
using System.Diagnostics;

namespace GitStatisticsAnalyzer.Windows
{
    /// <summary>
    /// Interaction logic for AuthorWindow.xaml
    /// </summary>
    public partial class AuthorWindow : Window, ICommandWindow
    {
        private string authorName;

        public AuthorWindow()
        {
            InitializeComponent();
        }

        public CommandFactory CommandFactory { get; set; }

        public string AuthorName
        {
            get
            {
                return authorName;
            }
            set
            {
                authorName = value;

                if (CommandFactory == null) return;
                AuthorNameChanged();
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            AuthorNameChanged();
        }

        private void AuthorNameChanged()
        {
            var command = CommandFactory.GetCommand<ListSimpleCommitsResult>($"--author=\"{AuthorName}\"");
            command.Execute();

            dataGrid.ItemsSource = command.Result.Commits;
        }

        /// <summary>
        /// This event is called, if the DataGrid shows a list of SimpleCommitResults.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimpleCommitDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            var selectedSimpleCommit = ((DataGrid)sender).SelectedItem as SimpleCommitResult;
            Debug.Assert(selectedSimpleCommit != null);
            new FullCommitView(CommandFactory, selectedSimpleCommit?.Id).Show();
        }
    }
}
