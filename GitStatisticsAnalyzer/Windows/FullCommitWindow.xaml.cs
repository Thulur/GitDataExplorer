using System.Threading.Tasks;
using System.Windows;

using GitStatisticsAnalyzer.Commands;
using GitStatisticsAnalyzer.Results.Commits;
using System.Windows.Controls;
using GitStatisticsAnalyzer.Files;

namespace GitStatisticsAnalyzer.Windows
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
            fileDataGrid.ItemsSource = result.Files;
        }

        private void DataGridMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedFile = ((DataGrid)sender).SelectedItem as IFile;

            if (selectedFile == null) return;

            new FileWindow(CommandFactory, selectedFile).Show();
        }
    }
}
