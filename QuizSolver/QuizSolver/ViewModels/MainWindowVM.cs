using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;
using QuizSolver.Model;
using System.Runtime.CompilerServices;

namespace QuizSolver
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private Model.Quiz quiz;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public ICommand OpenCommand { get; private set; }
        public ICommand QuitCommand { get; private set; }


        public Model.Quiz Quiz
        {
            get { return quiz; }
            set 
            { 
                quiz = value;
                OnPropertyChanged();
            }
        }

        public MainWindowVM()
        {
            Quiz = new Model.Quiz("Title", new ObservableCollection<Question> { new Question(1, "New question", new ObservableCollection<Answer> { new Answer(1, "Correct answer", true), new Answer(2, "Incorrect answer", false) }) });
            
            OpenCommand = new BasicCommand(this.OpenExplorer);
            QuitCommand = new BasicCommand(this.QuitApplication);
        }

        private void UpdateQuestionNumbers()
        {
            for (int i = 0; i < Quiz.Questions.Count(); i++)
                Quiz.Questions[i].Number = i + 1;

        }


        // TODO: maybe implement an interface for commands without arguments?

        private void OpenExplorer(object ignorethis)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) { 
                // RODO: implement file reading
            }

            QuizViewVM QuizViewVM = new QuizViewVM(new Model.Quiz("bl", new ObservableCollection<Question>()));
            //QuizView QuizView = new Model.QuizView(QuizViewVM);
            //if (QuizView.ShowDialog() == true)
            //{
            //    Question output = new Question(selected.Number, QuizViewVM.Contents, QuizViewVM.Answers);
            //    Quiz.Questions[(int)index] = output;
            //}
        }

        private void QuitApplication(object ignorethis)
        {
            // TODO: move to model ?
            System.Windows.Application.Current.Shutdown();
        }

    }
}
