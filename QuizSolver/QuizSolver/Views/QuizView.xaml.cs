using System.Windows;

namespace QuizSolver
{
    /// <summary>
    /// Interaction logic for QuizView.xaml
    /// </summary>
    public partial class QuizView : Window
    {
        private readonly QuizViewVM vm;

        public QuizView(QuizViewVM vm)
        {
            this.vm = vm;
            this.DataContext = this.vm;
            InitializeComponent();
        }
    }
}
