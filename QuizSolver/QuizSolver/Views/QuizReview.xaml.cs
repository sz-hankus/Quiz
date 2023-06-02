using System.Windows;

namespace QuizSolver
{
    /// <summary>
    /// Interaction logic for QuizReview.xaml
    /// </summary>
    public partial class QuizReview : Window
    {
        private readonly QuizReviewVM vm;
        public QuizReview(QuizReviewVM vm)
        {
            this.vm = vm;
            this.DataContext = this.vm;
            InitializeComponent();
        }
    }
}
