using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuizSolver.Model
{
    public class Question : INotifyPropertyChanged
    {
        private int number;
		private String questionContents;
        private ObservableCollection<Answer> answers;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Question(int number, String questionContents, ObservableCollection<Answer> answers)
        {
            Number = number;
            QuestionContents = questionContents;
            Answers = answers;
        }

        public Question Copy()
        {
            ObservableCollection<Answer> answersCopy = new ObservableCollection<Answer>();
            foreach(Answer answer in answers)
            {
                answersCopy.Add(answer.Copy());
            }
            return new Question(Number, QuestionContents, answersCopy);
        }

        public int Number 
        {
            get { return number; }
            set 
            {
                number = value;
                OnPropertyChanged();
            }
        }

        public String QuestionContents
		{
			get { return questionContents; }
			set 
            { 
                questionContents = value;
                OnPropertyChanged();
            }
		}

		public ObservableCollection<Answer> Answers
		{
			get { return answers; }
			set { answers = value; }
		}

        public void AddAnswer(Answer answer)
		{
			if (!answers.Contains(answer))
				answers.Add(answer);
		}

        public void RemoveAnswer(Answer answer)
        {
			answers.Remove(answer);
        }

        public override String ToString()
		{
            //{ String.Join(" ", answers.ToArray())}
            return $"{number}. {questionContents}";
		}

        public bool CompareAnswers(Question q)
        {   
            // Can be done simpler and cleaner
            for( int i = 0; i < answers.Count; i++)
            {
                if (answers[i].Correct != q.answers[i].Correct)
                    return false;
            }
            return true;
        }
	}
}
