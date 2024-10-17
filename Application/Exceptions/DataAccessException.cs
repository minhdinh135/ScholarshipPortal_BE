using System.Runtime.Serialization;

namespace Application.Exceptions;

public class DataAccessException : Exception
{
    public DataAccessException()
    {
    }

    protected DataAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DataAccessException(string? message) : base(message)
    {
    }

    public DataAccessException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}