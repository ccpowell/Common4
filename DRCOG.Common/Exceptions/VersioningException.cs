using System;
using DRCOG.Common.Domain;

namespace DRCOG.Common.Exceptions
{
    /// <summary>
    /// An exception caused by an <see cref="IVersionable{VType}"/> conflict.
    /// </summary>
    public class VersioningException : BusinessRuleException
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public VersioningException() : base("The record being updated has changed since viewing. Please refresh the data and try again.") { }
        /// <summary>
        /// Instantiates this class with a specific error message.
        /// </summary>
        /// <param name="message">The message for the exception.</param>
        public VersioningException(string message) : base(message) { }
        /// <summary>
        /// Instantiates this class with a specific error message and an inner <see cref="Exception"/>.
        /// </summary>
        /// <param name="message">The message for the exception</param>
        /// <param name="inner">The inner <see cref="Exception"/></param>
        public VersioningException(string message, Exception inner) : base(message, inner) { }
    }
}
