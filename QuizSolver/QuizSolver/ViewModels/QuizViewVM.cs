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
        private readonly Model.Quiz correctQuiz;

        private Model.Question currentQuestion;
        private int currentIndex;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand NextCommand { get; private set; }
        public ICommand PreviousCommand { get; private set; }

        public ICommand FinishCommand { get; private set; }


        public Model.Quiz Quiz
        {
            get { return quiz; }
            set
            { 
                this.quiz = value;
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
            this.correctQuiz = quiz.Copy();
            this.WipeAnswers();
            this.currentQuestion = Quiz.Questions[0];
            this.currentIndex = 0;
            NextCommand = new BasicCommand(GoToNextQuestion);
            PreviousCommand = new BasicCommand(GoToPreviousQuestion);
            FinishCommand = new BasicCommand(FinishQuiz);
        }

        public QuizViewVM(Model.Quiz quiz)
        {

            this.quiz = quiz;
            this.correctQuiz = quiz.Copy();
            MessageBox.Show($"Poprawna odpowiedz w pierwszym bezposrednio po utworzeniu VM: \n {correctQuiz.Questions[0].Answers[1].Correct.ToString()}");  // delete later
            this.WipeAnswers();
            MessageBox.Show($"Poprawna odpowiedz po wipe answers: \n {correctQuiz.Questions[0].Answers[1].Correct.ToString()}"); // delete later
            this.currentQuestion = quiz.Questions[0];
            this.currentIndex = 0;
            NextCommand = new BasicCommand(GoToNextQuestion, IsNotAtEnd);
            PreviousCommand = new BasicCommand(GoToPreviousQuestion, IsNotAtStart);
            FinishCommand = new BasicCommand(FinishQuiz);
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

        private void FinishQuiz(object ignorethis)
        {
            MessageBox.Show(AssessQuiz().ToString());
            MessageBox.Show(quiz.Questions[0].Answers[1].Correct.ToString());
            MessageBox.Show(correctQuiz.Questions[0].Answers[1].Correct.ToString());

            OnPropertyChanged("Finish");
        }
        private int AssessQuiz()
        {
            int correctAnswers = 0;
            for(int i=0; i < quiz.Questions.LongCount(); i++)
            {
                if(quiz.Questions[i].CompareAnswers(correctQuiz.Questions[i]))
                    correctAnswers++;
            }
            return correctAnswers;
        }

        private bool IsNotAtStart(object ignorethis)
        {
            return currentIndex > 0;
        }

        private bool IsNotAtEnd(object ignorethis)
        {
            return currentIndex < Quiz.Questions.Count - 1;
        }
        private void WipeAnswers()
        {
            foreach (var question in this.quiz.Questions)
            {
                foreach (var asnwer in question.Answers)
                {
                    asnwer.Correct = false;
                }
            }
        }
    }
}
