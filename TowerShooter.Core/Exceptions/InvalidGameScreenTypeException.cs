using System;

namespace TowerShooter.Exceptions
{
    [Serializable]
    public class InvalidGameScreenTypeException : Exception
    {
        public InvalidGameScreenTypeException() { }
        public InvalidGameScreenTypeException(string message) : base(message) { }
        public InvalidGameScreenTypeException(string message, Exception inner) : base(message, inner) { }
        protected InvalidGameScreenTypeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}