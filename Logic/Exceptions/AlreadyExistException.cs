using System;


namespace Logic.Exceptions
{
    public class AlreadyExistException(string message) : Exception(message)
    {
    }
}
