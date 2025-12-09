namespace Sisusa.MVVM
{
    internal static class PrivateTaskExtensions
    {
        /// <summary>
        /// Converts a <see cref="Func"/> that takes only a <see cref="CancellationToken"/> and returns a <see cref="Task"/>
        /// </summary>
        /// <typeparam name="T">The typename of the parameter to accept.</typeparam>
        /// <param name="paramLessTask">The Func to convert.</param>
        /// <returns>A func that passes the signature.</returns>
        /// <remarks>
        /// Introduced parameter is ignored! It is only to make the Func match the signature.
        /// </remarks>
        internal static Func<T?, CancellationToken, Task> ToFuncT<T>(this Func<CancellationToken, Task> paramLessTask)
        {
            return (param, token) => paramLessTask(token);
        }

        /// <summary>
        /// Convert a given parameterless bool returning function to one that takes a parameter.
        /// </summary>
        /// <typeparam name="T">The type of parameter to take.</typeparam>
        /// <param name="paramLessFunc">The function to wrap.</param>
        /// <returns>Function that takes a parameter and returns a boolean.</returns>
        /// <remarks>
        /// The parameter is ignored, it is there just to match signature!
        /// </remarks>
        internal static Func<T?, bool> ToFuncT<T>(this Func<bool> paramLessFunc)
        {
            return (param) => paramLessFunc();
        }
    }
}
