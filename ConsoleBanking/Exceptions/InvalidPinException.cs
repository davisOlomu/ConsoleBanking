using System;

namespace ConsoleBanking
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    internal class InvalidPinException : Exception
    {
        public InvalidPinException() { }
        public InvalidPinException(string message) : base(message) { }
        public InvalidPinException(string message, Exception inner) : base(message, inner) { }
        protected InvalidPinException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

