using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Quiz.Model
{
    public class Answer : INotifyPropertyChanged
    {
        int number;
        private String contents;
        private Boolean correct;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Answer(int number, String contents, Boolean correct)
        {
            Number = number;
            Contents = contents;
            Correct = correct;
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

        public String Contents
        {
            get { return contents; }
            set 
            { 
                contents = value;
                OnPropertyChanged();
            }
        }
        public Boolean Correct
        { 
            get { return correct; }
            set 
            {  
                correct = value;
                OnPropertyChanged();
            }
        }
    }
}
