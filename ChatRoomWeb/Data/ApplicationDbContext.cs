using Microsoft.EntityFrameworkCore;
using ChatRoomWeb.Models;

namespace ChatRoomWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ChatModel> Chats { get; set; }
    }
}
