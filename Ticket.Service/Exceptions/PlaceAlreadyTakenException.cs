namespace Ticket.Service.Exceptions;

public class PlaceAlreadyTakenException : Exception
{
    public PlaceAlreadyTakenException(string message) : base(message)
    { }
}