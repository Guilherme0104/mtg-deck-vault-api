
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
        public Guid UserId { get; set; }
        public List<Review> Reviews { get; set; } = new();

    }
}
