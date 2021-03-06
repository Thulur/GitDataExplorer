﻿using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using GitDataExplorer.Commands;
using GitDataExplorer.Results.Commits;
using GitDataExplorer.Files;

namespace GitDataExplorer.Windows
{
    /// <summary>
    /// Interaction logic for FullCommitView.xaml
    /// </summary>
    public partial class FullCommitWindow : ICommitWindow
    {
        public FullCommitWindow(CommandFactory commandFactory, string id)
        {
            InitializeComponent();
            CommandFactory = commandFactory;
            Id = id;
        }

        public CommandFactory CommandFactory { get; }

        public string Id { get; }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            var command = CommandFactory.GetCommand<FullCommitResult>();
            command.Parameters = Id;
            await Task.Run(() => command.Execute());

            var result = command.Result;
            Title = result.Title + " (" + result.Id + ") ";
            commitTextBox.Text = result.Id;
            authorTextBox.Text = result.Author.ToString();
            emailTextBox.Text = result.Author.Email;
            dateTextBox.Text = result.Date.ToShortDateString();
            fileDataGrid.ItemsSource = result.Files.Where(f => f.FileState != FileState.DELETED);
        }

        private void DataGridMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedFile = ((DataGrid)sender).SelectedItem as IFile;

            if (selectedFile == null) return;

            new FileWindow(CommandFactory, selectedFile).Show();
        }
    }
}
