using Microsoft.EntityFrameworkCore;

namespace Atlas.Server;

public class AtlasDbContext : DbContext
{
    public AtlasDbContext(DbContextOptions<AtlasDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}
