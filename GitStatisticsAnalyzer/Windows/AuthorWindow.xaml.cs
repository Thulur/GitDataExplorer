using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

using GitStatisticsAnalyzer.Data;
using GitStatisticsAnalyzer.Commands;
using GitStatisticsAnalyzer.Results;
using GitStatisticsAnalyzer.Results.Commits;

namespace GitStatisticsAnalyzer.Windows
{
    /// <summary>
    /// Interaction logic for AuthorWindow.xaml
    /// </summary>
    public partial class AuthorWindow : Window, ICommandWindow
    {
        private Author author;

        public AuthorWindow()
        {
            InitializeComponent();
        }

        public CommandFactory CommandFactory { get; set; }

        public Author Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;

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
            var command = CommandFactory.GetCommand<ListSimpleCommitsResult>($"--author=\"{Author}\"");
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
