using System.Threading.Tasks;
using System.Windows;

using GitStatisticsAnalyzer.Commands;
using GitStatisticsAnalyzer.Results.Commits;

namespace GitStatisticsAnalyzer.Windows
{
    /// <summary>
    /// Interaction logic for FullCommitView.xaml
    /// </summary>
    public partial class FullCommitView : ICommitWindow
    {
        public FullCommitView(CommandFactory commandFactory, string id)
        {
            InitializeComponent();
            CommandFactory = commandFactory;
            Id = id;
        }

        public CommandFactory CommandFactory { get; }

        public string Id { get; }

        private async void _WindowLoaded(object sender, RoutedEventArgs e)
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
    }
}
