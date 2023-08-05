namespace scenario10API.models.DTOs
{
    public class UploadFileDto
    {
        public string URL { get; set; }

        public UploadFileDto(string url = "")
        {
            URL = url;

        }
    }
}
