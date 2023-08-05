namespace scenario10API.models.DTOs
{
    public class TokenDTO
    {
        public string Token { get; init; } = string.Empty;
        public DateTime ExpiryDate { get; init; }

        public string Id { get; init; } = string.Empty;

        public string? Title { get; init; } = string.Empty;
    }
}
