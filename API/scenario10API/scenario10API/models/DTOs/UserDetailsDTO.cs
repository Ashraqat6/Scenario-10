namespace scenario10API.models.DTOs
{
    public class UserDetailsDTO
    {
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public List<ReportDTO> Reports { get; set; }

    }
}
