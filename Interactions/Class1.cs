namespace Sisusa.MVVM.Interactions
{
    internal class Class1
    {
    }

    public interface IInteractionService
    {
        /// <summary>
        /// Get a notification service to inform the user or ask for input.
        /// </summary>
        INotificationService Notification { get; }
    }
}
