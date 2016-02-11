using System;
using System.Runtime.Serialization;

namespace SeriusSoft.Frameworks.MVVM.Exceptions
{
  public class NotInBatchModeException : Exception
  {
    public NotInBatchModeException() : base() { }

    public NotInBatchModeException(string message) : base(message) { }

    public NotInBatchModeException(string message, Exception innerException) : base(message, innerException) { }

    public NotInBatchModeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
  }
}
