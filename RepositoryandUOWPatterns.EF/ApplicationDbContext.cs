using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RepositoryandUOWPatterns.Core.Entities;

namespace RepositoryandUOWPatterns.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
