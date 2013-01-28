using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRCOG.Common.Exceptions
{
    public class RecordNotFoundException : DataException
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public RecordNotFoundException() : base("Record Not Found!") { }
        /// <summary>
        /// Instantiates this class with a specific error message.
        /// </summary>
        /// <param name="message">The message for the exception.</param>
        public RecordNotFoundException(string message) : base(message) { }
        /// <summary>
        /// Instantiates this class with a specific error message and an inner <see cref="Exception"/>.
        /// </summary>
        /// <param name="message">The message for the exception</param>
        /// <param name="inner">The inner <see cref="Exception"/></param>
        public RecordNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
