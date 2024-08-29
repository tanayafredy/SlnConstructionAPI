using Construction.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Construction.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ConstructionProject> ConstructionProjects { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
