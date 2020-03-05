using Microsoft.EntityFrameworkCore;

namespace WebApp.Model
{
    public class MyDbContext : DbContext
    {

        public DbSet<Participant> Participants { get; set; }
        public DbSet<Project> Projects { get; set; }

        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}