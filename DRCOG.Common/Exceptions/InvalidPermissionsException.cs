using System;

namespace DRCOG.Common.Exceptions
{
    /// <summary>
    /// An exeption caused when a user attempts to modify an object without sufficient permissions to do so.
    /// </summary>
    public class InvalidPermissionsException : BusinessRuleException
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        protected InvalidPermissionsException() : base() { }
        /// <summary>
        /// Instantiates this class with a specific error message.
        /// </summary>
        /// <param name="message">The message for the exception.</param>
        public InvalidPermissionsException(string message) : base(message) { }
        /// <summary>
        /// Instantiates this class with a specific error message and an inner <see cref="Exception"/>.
        /// </summary>
        /// <param name="message">The message for the exception</param>
        /// <param name="inner">The inner <see cref="Exception"/></param>
        public InvalidPermissionsException(string message, Exception inner) : base(message, inner) { }
    }
}
