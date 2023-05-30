using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Quiz.Model;

namespace Quiz
{
    public class QuizViewVM
    {
        private Model.Quiz quiz;
        public ICommand BeginCommand { get; private set; }
        public ICommand FinishCommand { get; private set; }

        public Model.Quiz Quiz
        {
            get { return quiz; }
            set { quiz = value; }
        }


        public QuizViewVM(Model.Quiz quiz)
        {
            this.quiz = quiz;
            BeginCommand = new BasicCommand(BeginQuiz);
            FinishCommand = new BasicCommand(FinishQuiz);
        }

        private void BeginQuiz(object ignorethis)
        {
            // TO DO
        }
        private void FinishQuiz(object ignorethis)
        {
            // TO DO
        }
    }
}
