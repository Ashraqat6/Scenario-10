using Microsoft.AspNetCore.Identity;

namespace scenario10API.models
{
    public class User:IdentityUser
    {
        public ICollection<Report> Reports { get; set; } = new HashSet<Report>();

    }
}
