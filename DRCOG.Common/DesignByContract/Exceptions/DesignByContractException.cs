using System;

namespace DRCOG.Common.DesignByContract.Exceptions
{
    /// <summary>
    /// Exception raised when a contract is broken.
    /// Catch this exception type if you wish to differentiate between 
    /// any DesignByContract exception and other runtime exceptions.
    ///  
    /// </summary>
    public class DesignByContractException : Exception
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        protected DesignByContractException() { }
        /// <summary>
        /// Instantiates this class with a specific error message.
        /// </summary>
        /// <param name="message">The message for the exception.</param>
        protected DesignByContractException(string message) : base(message) { }
        /// <summary>
        /// Instantiates this class with a specific error message and an inner <see cref="Exception"/>.
        /// </summary>
        /// <param name="message">The message for the exception</param>
        /// <param name="inner">The inner <see cref="Exception"/></param>
        protected DesignByContractException(string message, Exception inner) : base(message, inner) { }
    }
}
