using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using QuizCreator.Model;
using System.Runtime.CompilerServices;
using System.Windows;

namespace QuizCreator
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private Model.Quiz quiz;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand ModifyCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }



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
            AddCommand = new BasicCommand(this.AddQuestion);
            DeleteCommand = new BasicCommand(this.DeleteQuestion);
            ModifyCommand = new BasicCommand(this.ModifyQuestion);
            SaveCommand = new BasicCommand(this.SaveQuiz);  
            LoadCommand = new BasicCommand(this.LoadQuiz);
        }

        private void UpdateQuestionNumbers()
        {
            for (int i = 0; i < Quiz.Questions.Count(); i++)
                Quiz.Questions[i].Number = i + 1;

        }


        // TODO: maybe implement an interface for commands without arguments?
        private void AddQuestion(object ignorethis)
        {
            int lastNumber = Quiz.Questions.Count != 0 ? Quiz.Questions.Last().Number : 0;
            Question newQuestion = new Question(lastNumber + 1, "New question", new ObservableCollection<Answer> { new Answer(1, "Correct answer", true), new Answer(2, "Incorrect answer", false) }); 

            QuestionWizardVM questionWizardVM = new QuestionWizardVM(newQuestion);
            QuestionWizard questionWizard = new QuestionWizard(questionWizardVM);
            if (questionWizard.ShowDialog() == true)
            {
                Question output = questionWizardVM.Question;
                this.Quiz.Questions.Add(output);
            }
        }

        private void DeleteQuestion(object index)
        {
            if ((int)index == -1)
                return;
            Quiz.Questions.RemoveAt((int)index);
            UpdateQuestionNumbers();
        }

        private void ModifyQuestion(object index)
        {
            if ((int)index == -1)
                return;
            Question selected = Quiz.Questions.ElementAt((int)index).Copy();

            QuestionWizardVM questionWizardVM = new QuestionWizardVM(selected);
            QuestionWizard questionWizard = new QuestionWizard(questionWizardVM);
            if (questionWizard.ShowDialog() == true)
            {
                Question output = new Question(selected.Number, questionWizardVM.Contents, questionWizardVM.Answers);
                Quiz.Questions[(int)index] = output;
            }
        }

        private void SaveQuiz(object ignorethis)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = false;
            openFileDialog.FileName = $"{this.Quiz.Name}.sqlite";
            if (openFileDialog.ShowDialog() != true)
                return;

            Model.DataBaseManager.SaveQuizToDB(Quiz, openFileDialog.FileName);
        }

        private void LoadQuiz(object ignorethis)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            if (openFileDialog.ShowDialog() != true)
                return;

            try
            {
                Quiz = Model.DataBaseManager.ReadQuizFromDB(openFileDialog.FileName);
            }
            catch (System.Exception)
            {
                MessageBox.Show($"Unable to load a quiz from {openFileDialog.FileName}", "Loading error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
