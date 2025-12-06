using System.Windows.Input;

namespace Sisusa.MVVM
{
    /// <summary>
    /// A command that relays its functionality to other objects by invoking delegates, with a parameter of type T.
    /// </summary>
    /// <typeparam name="T"> The datatype of the parameter passed to the command methods.
    /// </typeparam>
    public class RelayCommand<T> : ICommand
    {
        /// <summary>
        /// Event raised when changes affect whether the command can execute.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        private readonly Action<T> _exectute;
        private readonly Func<T, bool>? _canExecute = _ => true;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class with the specified execution logic and optional    
        /// </summary>
        /// <param name="execute">The action to execute when the command is invoked/fired.</param>
        /// <param name="canExecute">Function that determines whether the command can be executed.</param>
        /// <exception cref="ArgumentNullException">If the action to execute/perform is null.</exception>
        public RelayCommand(Action<T> execute, Func<T, bool>? canExecute = null)
        {
            _exectute = execute ?? throw new ArgumentNullException(nameof(execute), "Action to be executed must be given.");
            _canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Optional pararemter used by the command.</param>
        /// <returns><see langword="true"/> if the wrapped action can be performed, <see langword="false" /> otherwise.</returns>
        public bool CanExecute(object? parameter)
        {
            if (_canExecute is null)
                return true;
            return _canExecute((T)parameter!);
        }

        /// <summary>
        /// Executes the associated action if the command is currently enabled.
        /// </summary>
        /// <remarks>This method invokes the command's action only if <see cref="CanExecute"/> returns <see langword="true"/>.</remarks>
        /// <param name="parameter">Optional parameter to be passed to the action.</param>
        public void Execute(object? parameter)
        {
            if (CanExecute(parameter))
                _exectute((T)parameter!);
        }

        /// <summary>
        /// Fires the CanExecuteChanged event to signal that the ability to execute the command may have changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }


    /// <summary>
    /// A command that relays its functionality to other objects by invoking delegates.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Event raised when changes affect whether the command can execute.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        private readonly Action _execute;
        private readonly Func<bool>? _canExecute = () => true;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class with the specified execution logic and optional    
        /// </summary>
        /// <param name="execute">The action to execute when the command is invoked/fired.</param>
        /// <param name="canExecute">Function that determines whether the command can be executed.</param>
        /// <exception cref="ArgumentNullException">If the action to execute/perform is null.</exception>
        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute), "Action to be executed must be given.");
            _canExecute = canExecute;
            
        }

        /// <summary>
        /// Determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Optional pararemter used by the command.</param>
        /// <returns><see langword="true"/> if the wrapped action can be performed, <see langword="false" /> otherwise.</returns>
        public bool CanExecute(object? parameter)
        {
            if (_canExecute is null)
                return true;
            return _canExecute();
        }

        /// <summary>
        /// Executes the associated action if the command is currently enabled.
        /// </summary>
        /// <remarks>This method invokes the command's action only if <see cref="CanExecute"/> returns <see langword="true"/>.</remarks>
        /// <param name="parameter">Optional parameter to be passed to the action.</param>
        public void Execute(object? parameter)
        {
            if (CanExecute(parameter))
                _execute();
        }

        /// <summary>
        /// Fires the CanExecuteChanged event to signal that the ability to execute the command may have changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
