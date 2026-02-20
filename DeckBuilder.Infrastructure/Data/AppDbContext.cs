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

            // Configuração JSONB
            modelBuilder.Entity<Deck>()
                .Property(d => d.CardList)
                .HasColumnType("jsonb");

            // Relacionamento Deck -> User
            modelBuilder.Entity<Deck>()
                .HasOne(d => d.User)           
                .WithMany(u => u.Decks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento Review -> Deck
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Deck)
                .WithMany(d => d.Reviews)
                .HasForeignKey(r => r.DeckId)
                .OnDelete(DeleteBehavior.Cascade);


            // Relacionamento Review -> User
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany() 
                .HasForeignKey(r => r.UserId);

            // Configuração da Propriedade Rating
            modelBuilder.Entity<Review>()
                    .Property(r => r.Rating)
                    .IsRequired();
        }


    }
}
