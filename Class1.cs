using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Sisusa.MVVM
{
    
    /// <summary>
    /// Specifies an object that supports undoing actions.
    /// </summary>
    public interface IUndoableAction
    {
        void Undo();
    }

    public static class RelayCommandFactory
    {
        public static RelayCommand GetCommand(Action action, Func<bool>? predicate = null)
        {
            return new RelayCommand(action, predicate);
        }

        public static RelayCommand<T> GetCommand<T>(Action<T> action, Func<T, bool>? predicate = null)
        {
            return new RelayCommand<T>(action, predicate);
        }

        public static AsyncRelayCommand GetAsyncCommand(Func<CancellationToken, Task> actionAsync,
            IErrorHandler errorHandler, Func<bool>? predicate = null)
        {
            return new AsyncRelayCommand(actionAsync, errorHandler, predicate);
        }

        public static AsyncRelayCommmand<T> GetAsyncCommand<T>(Func<T?, CancellationToken, Task> actionAsync,
            IErrorHandler errorHandler,
            Func<T?, bool>? predicate = null)
        {
            return new AsyncRelayCommmand<T>(actionAsync, errorHandler, predicate);
        }
    }

    /**
    public static class TaskExtensions
    {
        public static async void FireAndForget(this Func<Task> actionTask, IErrorHandler errorHandler)
        {
            try
            {
                await actionTask();
            }
            catch(Exception e)
            {
                errorHandler.Handle(e);
            }
        }

        public static void SafeFireAndForget(this Func<CancellationToken, Task> actionTask,IErrorHandler errorHandler, CancellationToken cancellationToken )
        {
            try
            {
                _ = actionTask(cancellationToken);
            }
            catch (Exception e)
            {
                errorHandler.Handle(e);
            }
        }
    }
    **/
}
