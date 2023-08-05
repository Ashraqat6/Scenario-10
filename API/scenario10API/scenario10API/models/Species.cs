using System.Net.Sockets;

namespace scenario10API.models
{
    public class Species
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string ScientificName { get; set; } = "";
        public string Status { get; set; }= "";
        public string Img { get; set; } = "";
        public ICollection<Report> Reports { get; set; } = new HashSet<Report>();


    }
}
