using System;

namespace DRCOG.Common.Exceptions
{
    /// <summary>
    /// Occurs when a record cannot be changed or removed becasue it has exhisting relations.
    /// </summary>
    public class ReferentialIntegrityException : DataException
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        protected ReferentialIntegrityException() : base() { }
        /// <summary>
        /// Instantiates this class with a specific error message.
        /// </summary>
        /// <param name="message">The message for the exception.</param>
        public ReferentialIntegrityException(string message) : base(message) { }
        /// <summary>
        /// Instantiates this class with a specific error message and an inner <see cref="Exception"/>.
        /// </summary>
        /// <param name="message">The message for the exception</param>
        /// <param name="inner">The inner <see cref="Exception"/></param>
        public ReferentialIntegrityException(string message, Exception inner) : base(message, inner) { }
    }
}
