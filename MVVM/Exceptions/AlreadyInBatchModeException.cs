using System;
using System.Runtime.Serialization;

namespace SeriusSoft.Frameworks.MVVM.Exceptions
{
  public class AlreadyInBatchModeException : Exception
  {
    public AlreadyInBatchModeException() : base() { }

    public AlreadyInBatchModeException(string message) : base(message) { }

    public AlreadyInBatchModeException(string message, Exception innerException) : base(message, innerException) { }

    public AlreadyInBatchModeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
  }
}
