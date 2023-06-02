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
