using System.Windows;

namespace QuizCreator
{
    /// <summary>
    /// Interaction logic for QuestionWizard.xaml
    /// </summary>
    public partial class QuestionWizard : Window
    {
        private readonly QuestionWizardVM vm;
        public QuestionWizard(QuestionWizardVM vm)
        {
            this.vm = vm;
            this.DataContext = this.vm;
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        
    }
}
