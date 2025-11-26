namespace Sisusa.MVVM.Interactions
{
    public interface IFontPickerService
    {
        /// <summary>
        /// Show a font picker dialog to the user.
        /// </summary>
        /// <param name="initialFont">The initial font to show in the font picker.</param>
        /// <returns>Task containing the font picked by the user, or null if the user cancelled.</returns>
        Task<string?> PickFontAsync(string initialFont);
    }
}
