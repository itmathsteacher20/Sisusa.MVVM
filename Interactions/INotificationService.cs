namespace Sisusa.MVVM.Interactions
{
    /// <summary>
    /// A service for showing notifications to the user.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Inform the user with a message, optionally providing an action they can take.
        /// </summary>
        /// <param name="message">Message informing the user about something.</param>
        /// <param name="actionLabel">If an action is to be taken or can be taken, the label or description of the action.</param>
        /// <param name="action">If an action can be performed in response to the message, specifies the action to perform.</param>
        /// <returns>Task representing the action.</returns>
        Task InformAsync(string message, string? actionLabel = null, Func<Task>? action = null);

        /// <summary>
        /// Ask the user for string input.
        /// </summary>
        /// <param name="message">Message explaining the input required.</param>
        /// <returns>Task containing the input from the user(if any).</returns>
        Task<string?> PromptAsync(string message);

        /// <summary>
        /// As the user for input of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of input to get from user.</typeparam>
        /// <param name="message">Message explaining the input required.</param>
        /// <returns>Task containing the input acquired from the user (if any).</returns>
        Task<T?> PromptAsync<T>(string message);

        /// <summary>
        /// Ask the user to confirm an action.
        /// </summary>
        /// <param name="message">Explain the action being confirmed.</param>
        /// <param name="confirmLabel">Label for the confirmation action.</param>
        /// <param name="cancelLabel">Label for the cancel action.</param>
        /// <returns></returns>
        Task<bool> ConfirmAsync(string message, string? confirmLabel = null, string? cancelLabel = null);
    }
}
