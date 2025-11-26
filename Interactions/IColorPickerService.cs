namespace Sisusa.MVVM.Interactions
{
    /// <summary>
    /// A service for showing color pickers to the user.
    /// </summary>
    public interface IColorPickerService
    {
        /// <summary>
        /// Show a color picker dialog to the user.
        /// </summary>
        /// <param name="initialColor">The initial color to show in the color picker.</param>
        /// <returns>Task containing the color picked by the user, or null if the user cancelled.</returns>
        Task<string?> PickColorAsync(string initialColor);
    }
}
