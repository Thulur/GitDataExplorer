using System.Windows;

namespace GitStatisticsAnalyzer.Dialogs
{
    /// <summary>
    /// Interaction logic for BranchesDialog.xaml
    /// </summary>
    public partial class BranchesDialog : Window
    {
        public BranchesDialog()
        {
            InitializeComponent();
        }

        public string FirstBranch { get; set; } = "";

        public string SecondBranch { get; set; } = "";

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
