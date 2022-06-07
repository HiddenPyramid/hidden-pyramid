using System;

public class InvalidParamException : Exception
{
    public InvalidParamException()
    {
    }

    public InvalidParamException(string message)
        : base(message)
    {
    }

    public InvalidParamException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
