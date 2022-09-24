using Asseco.Models;
using Microsoft.EntityFrameworkCore;

namespace Asseco.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserInformation> UserInformation { get; set; } 
    }
}
