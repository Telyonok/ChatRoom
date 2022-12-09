using ChatRoomWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatRoomWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<UserSignUp> Users { get; set; }
    }
}
