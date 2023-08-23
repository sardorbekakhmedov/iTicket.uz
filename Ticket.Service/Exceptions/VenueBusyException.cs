namespace Ticket.Service.Exceptions;

public class VenueBusyException : Exception
{
    public VenueBusyException(string message) : base(message)
    { }
}