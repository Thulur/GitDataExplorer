using LiveCharts;
using LiveCharts.Wpf;
using System.Linq;
using System.Windows;

using GitStatisticsAnalyzer.Commands;
using GitStatisticsAnalyzer.Results.Commits;
using GitStatisticsAnalyzer.Windows;

namespace GitStatisticsAnalyzer.Graphs
{
    /// <summary>
    /// Interaction logic for AuthorCommitGraphWindow.xaml
    /// </summary>
    public partial class AuthorCommitGraphWindow : Window, ICommandWindow
    {
        public AuthorCommitGraphWindow()
        {
            InitializeComponent();
        }

        public CommandFactory CommandFactory { get; set; }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            var command = CommandFactory.GetCommand<AuthorCommitsResult>();
            command.Execute();

            cartesianChart.AxisX.Add(new Axis
            {
                Title = "Author",
                Labels = command.Result.AuthorCommits.Keys.ToArray()
            });

            cartesianChart.AxisY.Add(new Axis
            {
                Title = "# Commits",
                LabelFormatter = value => value.ToString("N")
            });

            cartesianChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "",
                    Values = new ChartValues<int>(command.Result.AuthorCommits.Values.ToArray())
                }
            };
        }
    }
}
