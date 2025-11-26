namespace Sisusa.MVVM.Interactions
{
    /// <summary>
    /// A service for showing time pickers to the user.
    /// </summary>
    public interface ITimePickerService
    {
        /// <summary>
        /// Show a time picker dialog to the user.
        /// </summary>
        /// <param name="initialTime">The initial time to show in the time picker.</param>
        /// <returns>Task containing the time picked by the user, or null if the user cancelled.</returns>
        Task<TimeOnly?> PickTimeAsync(TimeOnly initialTime);
    }
}
