using System;

namespace CSharpMiscLibrary.Exceptions
{
    /// <summary>
    /// Invalid equality custom exception.
    /// </summary>
    [Serializable()]
    public class InvalidEqualityException : System.Exception
    {
        public InvalidEqualityException() : base() { }
        public InvalidEqualityException(string message) : base(message) { }
        public InvalidEqualityException(string message, Exception inner) : base(message, inner) { }
        protected InvalidEqualityException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
