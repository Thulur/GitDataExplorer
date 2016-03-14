using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GitStatisticsAnalyzer.Commands;
using GitStatisticsAnalyzer.Results;
using GitStatisticsAnalyzer.Results.Commits;

namespace GitStatisticsAnalyzer.Views
{
    /// <summary>
    /// Interaction logic for FullCommitView.xaml
    /// </summary>
    public partial class FullCommitView : ICommitView
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
        }
    }
}
