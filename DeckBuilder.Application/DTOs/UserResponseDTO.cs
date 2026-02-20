
namespace DeckBuilder.Application.DTOs
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<DeckShortResponseDTO> Decks { get; set; } = new();
    }

    public record DeckShortResponseDTO(int Id, string Name, string Format);
}
