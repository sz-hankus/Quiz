using System;
using System.Windows.Input;

namespace QuizCreator
{
    public class BasicCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        
        public BasicCommand(Action<object> execute) 
        {
            _execute = execute;
            _canExecute = (object ignorethis) => { return true; };
        }
        public BasicCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
