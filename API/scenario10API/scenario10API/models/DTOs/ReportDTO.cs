namespace scenario10API.models.DTOs
{
    public class ReportDTO
    {
        public int Id { get; set; }
        public string Location { get; set; } = "";
        public string Img { get; set; } = "";
        public DateTime Date { get; set; }
        public string UserId { get; set; } = "";
        public int SpeciesId { get; set; }
    }
}
