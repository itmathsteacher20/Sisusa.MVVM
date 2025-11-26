using System.Windows.Input;

namespace Sisusa.MVVM;

/// <summary>
/// The base viewmodel class contains bare operations for a viewModel.
/// </summary>
public abstract class ViewModelBase : BindableObject
{
    /// <summary>
    /// Creates a new Command from the given params.
    /// </summary>
    /// <param name="action">The action to perform when the command is executed.</param>
    /// <param name="canExecute">Determines whether the action can be performed or not.</param>
    /// <returns>The created Command object.</returns>
    protected static ICommand CreateCommand(Action action, Func<bool>? canExecute = null)
    {
        return new RelayCommand(action, canExecute);
    }
        
    /// <summary>
    /// Creates a new Command that takes a parameter.
    /// </summary>
    /// <param name="action">The action to perform when the command is executed.</param>
    /// <param name="canExecute">Determines whether the action can be performed or not.</param>
    /// <typeparam name="T">Type of the parameter passed to the command action.</typeparam>
    /// <returns>The created Command object.</returns>
    protected static ICommand CreateCommand<T>(Action<T> action, Func<T, bool>? canExecute = null)
    {
        return new RelayCommand<T>(action, canExecute);
    }
}