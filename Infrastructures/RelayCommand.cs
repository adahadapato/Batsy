
namespace Batsy.Infrastructures;

using Batsy.Events;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    //public event EventHandler? CanExecuteChanged;
    private readonly Action<object?> _execute;
    private readonly Predicate<object> _canExecute;

    public RelayCommand(Action<object?> execute, Predicate<object> canExecute)
    {
        if (execute == null)
        {
            throw new ArgumentNullException("execute");
        }
        _canExecute = canExecute;
        _execute = execute;
    }

    public RelayCommand(Action<object> execute)
           : this(execute, null)
    {
        _execute = execute;
    }

    // Ensures WPF commanding infrastructure asks all RelayCommand objects whether their
    // associated views should be enabled whenever a command is invoked 
    public event EventHandler? CanExecuteChanged
    {
        add
        {
            CommandManager.RequerySuggested += value;
            //CanExecuteChangedInternal += value;
        }
        remove
        {
            CommandManager.RequerySuggested -= value;
            //CanExecuteChangedInternal -= value;
        }
    }
    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }


    public void Execute(object parameter)
    {
        _execute(parameter);
    }

    //private event EventHandler CanExecuteChangedInternal;

    //public void RaiseCanExecuteChanged()
    //{
    //    CanExecuteChangedInternal.Raise(this);
    //}
}
