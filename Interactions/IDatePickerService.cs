namespace Sisusa.MVVM.Interactions
{
    /// <summary>
    /// A service for showing date and time pickers to the user.
    /// </summary>
    public interface IDatePickerService
    {
        /// <summary>
        /// Shows a dialog to the user to pick a date.
        /// </summary>
        /// <param name="initialDate">The initial date to show on the picker.</param>
        /// <returns>Task containing the selected date(if any) or null if the user cancelled the dialog.</returns>
        Task<DateOnly?> PickDateAsync(DateOnly initialDate);

        /// <summary>
        /// Shows a dialog to the user to pick a time and a date.
        /// </summary>
        /// <param name="initialDateTime">The initial date and time to show.</param>
        /// <returns>Task containing the selcted date and time(if any) or null if the user cancelled the dialog.</returns>
        Task<DateTime?> PickDateTimeAsync(DateTime initialDateTime);
    }
}
