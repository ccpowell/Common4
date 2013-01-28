using System;

namespace DRCOG.Common.Exceptions
{
    /// <summary>
    /// Exception that occurrs when a Data contract is violeted.
    /// </summary>
    public class DataException : Exception
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        protected DataException() { }
        /// <summary>
        /// Instantiates this class with a specific error message.
        /// </summary>
        /// <param name="message">The message for the exception.</param>
        protected DataException(string message) : base(message) { }
        /// <summary>
        /// Instantiates this class with a specific error message and an inner <see cref="Exception"/>.
        /// </summary>
        /// <param name="message">The message for the exception</param>
        /// <param name="inner">The inner <see cref="Exception"/></param>
        protected DataException(string message, Exception inner) : base(message, inner) { }
    }
}
