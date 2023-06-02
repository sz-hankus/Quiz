using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuizSolver
{
    public class QuizReviewVM : INotifyPropertyChanged
    {
        private int score;
        private int numberOfQuestions;
        private int time;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public QuizReviewVM() { }
        public QuizReviewVM(int score, int numberOfQuestions, int time)
        {
            this.score = score;
            this.numberOfQuestions = numberOfQuestions;
            this.time = time;
        }
        public String Score
        {
            get { return $"{score} / {numberOfQuestions}"; }
        }

        public String Time
        {
            get { return $"Time: {time}s"; }
        }

    }
}
