using Ticket.Domain.Shared;

namespace Ticket.Domain.Entities;

public class Event : BaseEntity
{
    public required string Name { get; set; }
    public DateTime StarDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public decimal TicketPrice { get; set; }

    public uint VenueId { get; set; }
    public virtual Venue Venue { get; set; } = null!;
    public virtual List<Ticket>? Tickets { get; set; }
}




//name - tadbir nomi;
//start - boshlanish vaqti
//end - tugash vaqti
//price - bitta o'rindiq uchun bilet narhi