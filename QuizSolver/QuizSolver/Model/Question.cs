using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
	}
}
