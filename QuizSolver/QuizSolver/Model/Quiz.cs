using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuizSolver.Model
{
    public class Quiz : INotifyPropertyChanged
    {
		private String name;
		private ObservableCollection<Question> questions;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Quiz()
		{
			Name = "";
			questions = new ObservableCollection<Question>();
		}

		public Quiz(String name, ObservableCollection<Question> questions)
		{
			Name = name;
			Questions = questions;
		}

		public Quiz Copy()
		{
			ObservableCollection<Question> questionsCopy = new ObservableCollection<Question>();

			foreach (Question question in questions)
            {
				questionsCopy.Add(question.Copy());
            }
			return new Quiz(Name, questionsCopy);

		}

		public String Name
		{
			get { return name; }
			set 
			{ 
				name = value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<Question> Questions 
		{ 
			get { return questions; }
			set 
			{ 
				questions = value;
				OnPropertyChanged();
			}
		}
    }
}
