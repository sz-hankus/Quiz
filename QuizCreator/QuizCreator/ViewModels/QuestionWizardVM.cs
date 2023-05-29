using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using QuizCreator.Model;

namespace QuizCreator
{
    public class QuestionWizardVM
    {
        private Question question;
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }


        public Question Question
        {
            get { return question; }
            set { question = value; }
        }

        public String Contents
        {
            get { return question.QuestionContents; }
            set { question.QuestionContents = value; }
        }

        public ObservableCollection<Answer> Answers
        {
            get { return question.Answers; }
            set { question.Answers = value; }
        }

        public QuestionWizardVM() : this(new Question(0, "New question", new ObservableCollection<Answer> { new Answer(1, "Correct answer", true), new Answer(2, "Incorrect answer", false) }))
        { }

        public QuestionWizardVM(Question Question)
        {
            this.Question = Question;
            AddCommand = new BasicCommand(AddAnswer);
            DeleteCommand = new BasicCommand(DeleteAnswer);
        }

        private void AddAnswer(object ignorethis) 
        {
            Answers.Add(new Answer(Answers.Last().Number + 1, "", false));
        }

        private void DeleteAnswer(object index)
        {
            if ((int)index == -1)
                return;
            Question.Answers.RemoveAt((int)index);
            UpdateAnswersNumbers();
        }

        private void UpdateAnswersNumbers()
        {
            for (int i = 0; i < Answers.Count; i++)
                Answers[i].Number = i + 1;
        }
    }
}
