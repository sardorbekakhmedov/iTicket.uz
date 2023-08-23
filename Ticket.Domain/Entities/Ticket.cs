using Ticket.Domain.Shared;

namespace Ticket.Domain.Entities;

public class Ticket : BaseEntity
{
    public required string Username { get; set; }
    public uint RowNumber { get; set; }
    public uint PlaceNumber { get; set; }
    public required string SecretKey { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    public string? EventName { get; set; }
    public required uint EventId { get; set; }
    public virtual Event? Event { get; set; }
}

// - fullname - FIO
// - venueId - Tadbirning id'si (Tadbir joyi)
// - row - qator
// - seat - o'rindiq raqami
// - secret - mahfiy so'z