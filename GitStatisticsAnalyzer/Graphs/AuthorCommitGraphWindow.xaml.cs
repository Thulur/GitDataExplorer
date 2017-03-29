using LiveCharts;
using LiveCharts.Wpf;
using System.Linq;
using System.Windows;

using GitStatisticsAnalyzer.Commands;
using GitStatisticsAnalyzer.ResultCommandMapper;
using GitStatisticsAnalyzer.Results.Commits;
using GitStatisticsAnalyzer.Windows;

namespace GitStatisticsAnalyzer.Graphs
{
    /// <summary>
    /// Interaction logic for AuthorCommitGraphWindow.xaml
    /// </summary>
    public partial class AuthorCommitGraphWindow : Window, ICommandWindow
    {
        OptionalParameter optionalParameters = OptionalParameter.NONE;

        public AuthorCommitGraphWindow()
        {
            InitializeComponent();

            cartesianChart.AxisY.Add(new Axis
            {
                Title = "# Commits",
                LabelFormatter = value => value.ToString("N0")
            });
        }

        public CommandFactory CommandFactory { get; set; }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            UpdateChartValues();
        }

        private void ExcludeMergesCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            optionalParameters |= OptionalParameter.EXCLUDE_MERGES;
            UpdateChartValues();
        }

        private void ExcludeMergesCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            optionalParameters &= ~OptionalParameter.EXCLUDE_MERGES;
            UpdateChartValues();
        }

        private void AllBranchesCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            optionalParameters |= OptionalParameter.ALL;
            UpdateChartValues();
        }

        private void AllBranchesCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            optionalParameters &= ~OptionalParameter.ALL;
            UpdateChartValues();
        }

        private void UpdateChartValues()
        {
            var command = CommandFactory.GetCommand<AuthorCommitsResult>(optionalParameters);
            command.Execute();

            cartesianChart.AxisX.Clear();
            cartesianChart.AxisX.Add(new Axis
            {
                Title = "Author",
                Labels = command.Result.AuthorCommits.Keys.ToArray()
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

        private void CartesianChartDataClick(object sender, ChartPoint chartPoint)
        {
            var author = cartesianChart.AxisX[0].Labels[chartPoint.Key];
            var authorWindow = new AuthorWindow
            {
                AuthorName = author,
                Title = "Author: " + author
            };
            authorWindow.Show();
        }
    }
}
