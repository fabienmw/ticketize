using Microsoft.EntityFrameworkCore;
using Ticketize.Domain.Common;
using Ticketize.Domain.Entities;

namespace Ticketize.Persistence
{
    public class TicketizeDbContext : DbContext
    {
        public TicketizeDbContext(DbContextOptions<TicketizeDbContext> options) : base(options)
        {
                        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicketizeDbContext).Assembly);

            // seed data, added

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = "Concerts",
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = "Musicals",
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = "Plays",
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = "Conferences",
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModiefiedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
