namespace Sisusa.MVVM.Interactions
{
    /// <summary>
    /// Options for a file picker dialog.
    /// </summary>
    /// <param name="Title">The title on the dialog.</param>
    /// <param name="FileType">Specifies the type(extension) of files to pick or show.(*.ext|Description;) Default is all files.</param>
    /// <param name="InitialLocation">The starting location of the picker.</param>
    public record FilePickerOptions(string Title, string FileType="*.*|All Files", string InitialLocation="%userprofile%");
}
