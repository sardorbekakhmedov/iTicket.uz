using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Entities;
using Ticket.Domain.Shared;

namespace Ticket.Data.Context;

public class AppDbContext : DbContext
{
    public DbSet<Venue> Venues => Set<Venue>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Domain.Entities.Ticket> Tickets => Set<Domain.Entities.Ticket>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateTimeStampForBaseEntityClass();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimeStampForBaseEntityClass()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is not BaseEntity entity)
                continue;

            switch (entry.State)
            {
                case EntityState.Added:
                    entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }
    }
}