using System;

namespace DRCOG.Common.Exceptions
{
    public class UnitOfWorkException : Exception
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public UnitOfWorkException() { }
        /// <summary>
        /// Instantiates this class with a specific error message.
        /// </summary>
        /// <param name="message">The message for the exception.</param>
        public UnitOfWorkException(string message) : base(message) { }
        /// <summary>
        /// Instantiates this class with a specific error message and an inner <see cref="Exception"/>.
        /// </summary>
        /// <param name="message">The message for the exception</param>
        /// <param name="inner">The inner <see cref="Exception"/></param>
        public UnitOfWorkException(string message, Exception inner) : base(message, inner) { }
    }
}
