using System.Windows;

namespace Quiz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM vm = new MainWindowVM();

        public MainWindow()
        {
            this.DataContext = this.vm;
            InitializeComponent();
        }
    }
}
