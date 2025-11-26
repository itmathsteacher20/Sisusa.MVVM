namespace Sisusa.MVVM.Interactions
{
    /// <summary>
    /// A service for showing file and folder pickers to the user.
    /// </summary>
    public interface IFileDialogService
    {
        /// <summary>
        /// Display a file picker dialog to the user.
        /// </summary>
        /// <param name="options">Options specifying the type of file to choose, w.</param>
        /// <returns>The selected file or null if the dialog was cancelled.</returns>
        Task<string?> PickFileAsync(FilePickerOptions options);

        /// <summary>
        /// Displays a folder browser dialog to the user for the user to choose a folder from the file system.
        /// </summary>
        /// <param name="title">Title of the dialog.</param>
        /// <returns>Task containing the path to the selected folder(if any) or null if the dialog was cancelled.</returns>
        Task<string?> PickFolderAsync(string title);
    }
}
