using System;
using System.Windows.Input;

namespace PhoneCore.Framework.Views.Command
{
    /// <summary>
    /// Interactivity command
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExecuteCommand<T>: ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public ExecuteCommand(Action<T> execute):this(execute, null)
        {
        }

        public ExecuteCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute((T) parameter);
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if(CanExecute(parameter))
                _execute((T)parameter);
        }
    }
}
