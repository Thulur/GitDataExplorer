using System.Windows;

using GitStatisticsAnalyzer.Commands;

namespace GitStatisticsAnalyzer.Windows
{
    /// <summary>
    /// Interaction logic for AuthorWindow.xaml
    /// </summary>
    public partial class AuthorWindow : Window, ICommandWindow
    {
        private string authorName;

        public AuthorWindow()
        {
            InitializeComponent();
        }

        public CommandFactory CommandFactory { get; set; }

        public string AuthorName
        {
            get
            {
                return authorName;
            }
            set
            {
                authorName = value;
                AuthorNameChanged();
            }
        }

        private void AuthorNameChanged()
        {
            
        }
    }
}
