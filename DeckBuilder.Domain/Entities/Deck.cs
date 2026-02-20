
namespace DeckBuilder.Domain.Entities
{
    public class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Format { get; set; } = "Commander";
        public string CardList { get; set; } = "[]";
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        //chaves estrangeiras
        public Guid UserId { get; set; }

        //propriedades de navegação
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
