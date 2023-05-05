using System;

public class InvalidActionException : Exception
{
    public InvalidActionException(string message, string objName = "") : base(objName + ":" + message)
    { }
}
