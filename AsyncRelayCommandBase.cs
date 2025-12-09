namespace Sisusa.MVVM
{
    public abstract class AsyncRelayCommandBase : IDisposable
    {
        protected bool _isBusy;
        protected bool _isDisposing;
        protected CancellationTokenSource _cancellationTokenSource = new();

        /// <summary>
        /// Event raised when the ability to execute the command has changed.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Notify subscribers that the ability to execute the command has changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Cancel the async operation linked to this command.
        /// </summary>
        /// <remarks>
        /// This might not be immediate since the method simply asks the linked <see cref="Task"/> to cancel.
        /// This counts on the underlying <see cref="Task"/> being <see cref="CancellationToken"/> aware.
        /// If the <see cref="Task"/> ignores <see cref="CancellationToken"/> this call will have no effect.
        /// </remarks>
        public void Cancel()
        {
            if (_isBusy)
                _cancellationTokenSource.Cancel();
        }

        /// <summary>
        /// Whether the current instance is in a state where it can execute the linked Task.
        /// </summary>
        /// <typeparam name="T">Type of the parameter used.</typeparam>
        /// <param name="parameter">Parameter used to check status</param>
        /// <param name="canAct">Caller given function that decides whether the command can be executed or not.</param>
        /// <returns>True if the command can execute, False otherwise.</returns>
        /// <remarks>The parameter is often ignored.</remarks>
        protected virtual bool CanExecute<T>(T? parameter = default, Func<T?, bool>? canAct = null)
        {
            if (_isBusy || _isDisposing)
                return false;
            if (_cancellationTokenSource.IsCancellationRequested)
                return false;
            
            if (canAct is null)
                return true;

            return canAct(parameter);
        }

        /// <summary>
        /// Executes the given action, provided the command is not currently busy.
        /// Takes the parameter passes it to the action and uses the errorHandler given to handle errors.
        /// </summary>
        /// <typeparam name="T">The type of the parameter used by the command.</typeparam>
        /// <param name="parameter">The actual parameter passed to the Command.</param>
        /// <param name="action">The action to execute.</param>
        /// <param name="errorHandler">Error handler used to handle errors.</param>
        /// <returns>The task representing the command execution.</returns>
        protected virtual async Task ExecuteAsync<T>(T? parameter, Func<T?, CancellationToken, Task> action, IErrorHandler errorHandler)
        {
            if (!CanExecute<T>(parameter))
                return;

            _isBusy = true;
            RaiseCanExecuteChanged();
            try
            {
                await action(parameter, _cancellationTokenSource.Token);
            }
            catch(Exception ex)
            {
                errorHandler.Handle(ex);
            }
            finally
            {
                _isBusy = false;
                RaiseCanExecuteChanged();
            }
        }

        public void Dispose()
        {
            if (_isBusy)
                _cancellationTokenSource.Cancel(true);

            _isDisposing = true;
            GC.SuppressFinalize(this);
        }

    }
}
