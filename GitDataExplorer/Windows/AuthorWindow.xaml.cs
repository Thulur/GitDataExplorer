using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

using GitDataExplorer.Data;
using GitDataExplorer.Commands;
using GitDataExplorer.ResultCommandMapper;
using GitDataExplorer.Results;
using GitDataExplorer.Results.Commits;
using System;

namespace GitDataExplorer.Windows
{
    /// <summary>
    /// Interaction logic for AuthorWindow.xaml
    /// </summary>
    public partial class AuthorWindow : Window, ICommandWindow
    {
        OptionalParameter optionalParameters = OptionalParameter.NONE;
        private Author author;

        public AuthorWindow(CommandFactory commandFactory, Author author)
        {
            InitializeComponent();

            CommandFactory = commandFactory;
            Author = author;
        }

        public CommandFactory CommandFactory { get; private set; }

        public Author Author
        {
            get
            {
                return author;
            }
            private set
            {
                author = value;
                UpdateDataGrid();
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            if (CommandFactory == null) return;

            var command = CommandFactory.GetCommand<ListSimpleCommitsResult>(optionalParameters, $"--author=\"{Author}\"");
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
            new FullCommitWindow(CommandFactory, selectedSimpleCommit?.Id).Show();
        }

        private void ExcludeMergesCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            optionalParameters |= OptionalParameter.EXCLUDE_MERGES;
            UpdateDataGrid();
        }

        private void ExcludeMergesCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            optionalParameters &= ~OptionalParameter.EXCLUDE_MERGES;
            UpdateDataGrid();
        }
    }
}
