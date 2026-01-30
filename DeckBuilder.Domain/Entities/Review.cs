
namespace DeckBuilder.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; } // 1 a 5
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int DeckId { get; set; }
        public Guid UserId { get; set; }
    }
}
