using System.Runtime.Serialization;

namespace Application.Exceptions;

public class FileProcessingException : Exception
{
    public FileProcessingException()
    {
    }

    protected FileProcessingException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public FileProcessingException(string? message) : base(message)
    {
    }

    public FileProcessingException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}