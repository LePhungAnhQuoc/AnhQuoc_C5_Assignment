using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnhQuoc_C5_Assignment
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _canExecute = null;
            _execute = execute;
        }

        public RelayCommand(Predicate<T> canExecute, Action<T> execute)
            : this(execute)
        {
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            // Nếu không có thì mặc định là thực thi (true)
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public RelayCommand(Action<object> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _canExecute = null;
            _execute = execute;
        }

        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
            : this(execute)
        {
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            // Nếu không có thì mặc định là thực thi (true)
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #region TrienKhai-Event
        // Ensures WPF commanding infrastructure asks all RelayCommand objects whether their
        // associated views should be enabled whenever a command is invoked 
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        #endregion
    }
}
