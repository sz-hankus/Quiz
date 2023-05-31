 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QuizSolver.Model;

namespace QuizSolver
{
    public class QuizViewVM : INotifyPropertyChanged
    {
        private Model.Quiz quiz;
        private Model.Question currentQuestion;
        private int currentIndex;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand NextCommand { get; private set; }
        public ICommand PreviousCommand { get; private set; }

        public Model.Quiz Quiz
        {
            get { return quiz; }
            set
            { 
                quiz = value;
                OnPropertyChanged();
            }
        }

        public Model.Question CurrentQuesiton
        {
            get { return currentQuestion; }
            set 
            { 
                currentQuestion = value;
                OnPropertyChanged();
            }
        }

        public String Progress
        {
            get { return $"{CurrentQuesiton.Number} / {Quiz.Questions.Count}"; }
        }

        public QuizViewVM()
        {
            this.quiz = new Quiz("Test", new ObservableCollection<Question> { new Question(1, "test question", new ObservableCollection<Answer>() { new Answer(1, "test answer 1", true)}) });
            this.currentQuestion = Quiz.Questions[0];
            this.currentIndex = 0;
            NextCommand = new BasicCommand(GoToNextQuestion);
            PreviousCommand = new BasicCommand(GoToPreviousQuestion);
        }

        public QuizViewVM(Model.Quiz quiz)
        {
            this.quiz = quiz;
            this.currentQuestion = quiz.Questions[0];
            this.currentIndex = 0;
            NextCommand = new BasicCommand(GoToNextQuestion, IsNotAtEnd);
            PreviousCommand = new BasicCommand(GoToPreviousQuestion, IsNotAtStart);
        }

        private void GoToNextQuestion(object ignorethis)
        {
            currentIndex++;
            CurrentQuesiton = Quiz.Questions[currentIndex];
            OnPropertyChanged("Progress");
        }

        private void GoToPreviousQuestion(object ignorethis)
        {
            currentIndex--;
            CurrentQuesiton = Quiz.Questions[currentIndex];
            OnPropertyChanged("Progress");
        }

        private bool IsNotAtStart(object ignorethis)
        {
            return currentIndex > 0;
        }

        private bool IsNotAtEnd(object ignorethis)
        {
            return currentIndex < Quiz.Questions.Count - 1;
        }
    }
}
