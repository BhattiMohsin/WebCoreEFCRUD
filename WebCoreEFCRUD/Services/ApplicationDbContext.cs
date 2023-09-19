using Microsoft.EntityFrameworkCore;
using WebCoreEFCRUD.Models;


namespace WebCoreEFCRUD.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

    }
}
