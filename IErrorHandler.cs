namespace Sisusa.MVVM
{
    /// <summary>
    /// An object that handles Exceptions/Errors.
    /// </summary>
    public interface IErrorHandler
    {
        /// <summary>
        /// Invokes the error handling protocol implemented by the handler.
        /// </summary>
        /// <remarks>
        /// Handling an error can be Logging, Reporting or rethrowing though that messes up the call-stack info.
        /// </remarks>
        /// <param name="error">The error to handle.</param>
        void Handle(Exception error);
    }
}
