using Ticket.Domain.Shared;

namespace Ticket.Domain.Entities;

public class Venue : BaseEntity
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public int Rows { get; set; }
    public int SeatsInRow { get; set; }
    public int Capacity => (Rows * SeatsInRow);

    public virtual List<Event>? Events { get; set; }
}

//name - joyning nomi;
//address - manzil;
//capacity - odam sig'imi
//rows - qatorlar soni
//seatsInRow - qatordagi o'rindiqlar soni