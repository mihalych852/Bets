using Microsoft.EntityFrameworkCore;
using BetsService.Domain;

namespace BetsService.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<EventOutcomes> EventOutcomes { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Domain.Bets> Bets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EventsModelCreating(modelBuilder);
            EventOutcomesModelCreating(modelBuilder);
            BetsModelCreating(modelBuilder);
        }

        private void BetsModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Bets>().HasKey(e => e.Id);

            modelBuilder.Entity<Domain.Bets>().Property(b => b.CreatedDate).IsRequired();
            modelBuilder.Entity<Domain.Bets>().Property(b => b.BettorId).IsRequired();
            modelBuilder.Entity<Domain.Bets>().Property(b => b.Amount).IsRequired()
                .HasPrecision(10, 2);
            modelBuilder.Entity<Domain.Bets>().Property(b => b.CreatedBy).IsRequired()
                .HasMaxLength(60);

            modelBuilder.Entity<Domain.Bets>()
                .HasOne(r => r.EventOutcomes)
                .WithMany(b => b.Bets)
                .HasForeignKey(k => k.EventOutcomesId)
                .IsRequired();
        }

        private void EventsModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Events>().HasKey(e => e.Id);

            modelBuilder.Entity<Events>().Property(b => b.EventStartTime).IsRequired();
            modelBuilder.Entity<Events>().Property(b => b.BetsEndTime).IsRequired();
            modelBuilder.Entity<Events>().Property(b => b.CreatedDate).IsRequired();
            modelBuilder.Entity<Events>().Property(b => b.Description).IsRequired()
                .HasMaxLength(1000);
            modelBuilder.Entity<Events>().Property(b => b.CreatedBy).IsRequired()
                .HasMaxLength(60);
            modelBuilder.Entity<Events>().Property(b => b.ModifiedBy).HasMaxLength(60);
            modelBuilder.Entity<Events>().Property(b => b.DeletedBy).HasMaxLength(60);
        }

        private void EventOutcomesModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventOutcomes>().HasKey(e => e.Id);

            modelBuilder.Entity<EventOutcomes>().Property(b => b.CreatedDate).IsRequired();
            modelBuilder.Entity<EventOutcomes>().Property(b => b.BetsClosed).IsRequired();
            modelBuilder.Entity<EventOutcomes>().Property(b => b.Description).IsRequired()
                .HasMaxLength(1000);
            modelBuilder.Entity<EventOutcomes>().Property(b => b.CreatedBy).IsRequired()
                .HasMaxLength(60);
            modelBuilder.Entity<EventOutcomes>().Property(b => b.ModifiedBy).HasMaxLength(60);
            modelBuilder.Entity<EventOutcomes>().Property(b => b.DeletedBy).HasMaxLength(60);
            modelBuilder.Entity<EventOutcomes>().Property(b => b.Commision).IsRequired()
                .HasPrecision(3, 3);
            modelBuilder.Entity<EventOutcomes>().Property(b => b.CurrentOdd).IsRequired()
                .HasPrecision(5, 3);

            modelBuilder.Entity<EventOutcomes>()
                .HasOne(r => r.Event)
                .WithMany(b => b.EventOutcomes)
                .HasForeignKey(k => k.EventId)
                .IsRequired();
        }
    }
}
