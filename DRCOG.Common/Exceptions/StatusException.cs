using System;

namespace DRCOG.Common.Exceptions
{
    /// <summary>
    /// An exception caused when a user attempts to modify a record with a particular <see cref="Enums.Status"/>
    /// </summary>
    public class StatusException : BusinessRuleException
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        protected StatusException() : base() { }
        /// <summary>
        /// Instantiates this class with a specific error message.
        /// </summary>
        /// <param name="message">The message for the exception.</param>
        public StatusException(string message) : base(message) { }
        /// <summary>
        /// Instantiates this class with a specific error message and an inner <see cref="Exception"/>.
        /// </summary>
        /// <param name="message">The message for the exception</param>
        /// <param name="inner">The inner <see cref="Exception"/></param>
        public StatusException(string message, Exception inner) : base(message, inner) { }
    }
}
