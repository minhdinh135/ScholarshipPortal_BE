using System.Runtime.Serialization;

namespace Application.Exceptions;

public class DuplicateException : Exception
{
    public DuplicateException()
    {
    }

    protected DuplicateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DuplicateException(string? message) : base(message)
    {
    }

    public DuplicateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}