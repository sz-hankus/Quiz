using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Model
{
    public class Quiz : INotifyPropertyChanged
    {
		private String name;
		private ObservableCollection<Question> questions;

        public event PropertyChangedEventHandler PropertyChanged;

        public Quiz(String name, ObservableCollection<Question> questions)
		{
			Name = name;
			Questions = questions;
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

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
