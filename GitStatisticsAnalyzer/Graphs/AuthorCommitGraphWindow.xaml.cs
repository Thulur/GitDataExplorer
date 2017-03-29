using LiveCharts;
using LiveCharts.Wpf;
using System.Linq;
using System.Windows;

using GitStatisticsAnalyzer.Commands;
using GitStatisticsAnalyzer.Results.Commits;
using GitStatisticsAnalyzer.Windows;
using GitStatisticsAnalyzer.ResultCommandMapper;
using System;

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

            cartesianChart.AxisY.Add(new Axis
            {
                Title = "# Commits",
                LabelFormatter = value => value.ToString("N0")
            });
        }

        public CommandFactory CommandFactory { get; set; }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            var command = CommandFactory.GetCommand<AuthorCommitsResult>();

            command.Execute();
            UpdateChartValues(command.Result.AuthorCommits.Keys.ToArray(), command.Result.AuthorCommits.Values.ToArray());
        }

        private void ExcludeMergesCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            var optionalParameter = excludeMergesCheckBox.IsChecked ?? false ? OptionalParameter.EXCLUDE_MERGES : OptionalParameter.NONE;
            var command = CommandFactory.GetCommand<AuthorCommitsResult>(optionalParameter);

            command.Execute();
            UpdateChartValues(command.Result.AuthorCommits.Keys.ToArray(), command.Result.AuthorCommits.Values.ToArray());
        }

        private void UpdateChartValues(string[] names, int[] numberCommits)
        {
            cartesianChart.AxisX.Clear();
            cartesianChart.AxisX.Add(new Axis
            {
                Title = "Author",
                Labels = names
            });

            cartesianChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "",
                    Values = new ChartValues<int>(numberCommits)
                }
            };
        }
    }
}
