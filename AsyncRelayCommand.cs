using System.Windows.Input;

namespace Sisusa.MVVM
{


    /// <summary>
    /// A <see cref="RelayCommand"/> that takes a typed parameter of type {T} 
    /// and is able to execute async tasks.
    /// </summary>
    /// <typeparam name="T">The typename of the command's parameter.</typeparam>
    /// <param name="execute">The async method to be executed when the command is invoked.</param>
    /// <param name="errorHandler"></param>
    /// <param name="canExecute"></param>
    public class AsyncRelayCommmand<T>(Func<T?, CancellationToken, Task> execute, IErrorHandler errorHandler, Func<T?, bool>? canExecute = null) : AsyncRelayCommandBase, ICommand, IDisposable
    {
        
        private readonly Func<T?, CancellationToken, Task> _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        private readonly Func<T?, bool>? _canExecute = canExecute;
        private readonly IErrorHandler _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));

        public void Execute(object? parameter = null)
        {
            if (!CanExecute(parameter))
                return;

            _ = ExecuteAsync<T>((T?)parameter, _execute, _errorHandler);
        }

        public bool CanExecute(object? parameter = null)
        {
            return _canExecute is null ? 
                base.CanExecute(parameter) :
                base.CanExecute<T?>((T?)parameter, _canExecute);
        }

    }

    /// <summary>
    /// A <see cref="RelayCommand"/> that is able to execute async tasks.
    /// </summary>
    public class AsyncRelayCommand : AsyncRelayCommandBase, ICommand, IDisposable
    {
         
        private readonly Func<CancellationToken, Task> _action;
        private readonly Func<bool>? _canExecute;
        private readonly IErrorHandler _errorHandler;
              
        
        /// <summary>
        /// Initialises a new AsyncRelayCommand.
        /// </summary>
        /// <param name="action">The task to be executed when the command is invoked.</param>
        /// <param name="errorHandler">An error handler to handle errors/exceptions thrown while executing the action.</param>
        /// <param name="canAct">A function that is used to determine whether the command can execute or not.</param>
        /// <exception cref="ArgumentNullException">
        /// If the action or errorHandler are not given.
        /// </exception>
        /// <remarks>
        /// To support cancellation,action must accept a <see cref="CancellationToken"/> and also use it correctly.
        /// </remarks>
        public AsyncRelayCommand(Func<CancellationToken, Task> action, IErrorHandler errorHandler, Func<bool>? canAct = null)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action), "Command action must be provided.");
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler), "Error handler is required.");
            _canExecute = canAct;
        }

        public void Execute(object? parameter = null) 
        {
            _ = ExecuteAsync(null, _action.ToFuncT<object?>(), _errorHandler);
        }

        public bool CanExecute(object? parameter = null)
        {
            if (_canExecute == null)
                return base.CanExecute(parameter);

            return base.CanExecute<object?>(
                parameter,
                _canExecute.ToFuncT<object?>()
                );
        }
        
    }
}
