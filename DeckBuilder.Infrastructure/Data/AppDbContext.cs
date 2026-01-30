using Microsoft.EntityFrameworkCore;
using DeckBuilder.Domain.Entities;

namespace DeckBuilder.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opitions) : base(opitions) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Deck>()
                .Property(d => d.CardList)
                .HasColumnType("jsonb");

            modelBuilder.Entity<Deck>()
                .HasOne<User>()
                .WithMany(u => u.Decks)
                .HasForeignKey(d => d.UserId);

            modelBuilder.Entity<Review>()
                .HasOne<Deck>()
                .WithMany(d => d.Reviews)
                .HasForeignKey(r => r.DeckId);

        }


    }
}
