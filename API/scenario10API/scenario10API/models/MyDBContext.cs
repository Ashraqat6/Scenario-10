using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace scenario10API.models
{
    public class MyDBContext : IdentityDbContext<User>
    {
        public MyDBContext(DbContextOptions<MyDBContext> options)
        : base(options)
        {
        }
        public DbSet<Report> Reports => Set<Report>();
        public DbSet<Species> Species => Set<Species>();


    }
}
