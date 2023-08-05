namespace scenario10API.models.DTOs
{
    public class RegisterDTO
    {
        public string Mobile { get; set; } = null!;

        public string Name { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string Password { get; set; } = null!;
    }
}
