using System.Windows;
using System.Windows.Controls;

namespace QuizSolver
{
    /// <summary>
    /// Interaction logic for PasswordInput.xaml
    /// </summary>
    public partial class PasswordInput : Window
    {
        private PasswordInputVM vm;
        public PasswordInput(PasswordInputVM vm)
        {
            this.vm = vm;
            this.DataContext = this.vm;
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((PasswordInputVM)this.DataContext).Password = ((PasswordBox)sender).Password;
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
